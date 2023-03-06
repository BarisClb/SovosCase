using Hangfire;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SovosCase.Application.Interfaces;
using SovosCase.Application.Interfaces.Mongo;
using SovosCase.Application.Models.Events;
using SovosCase.Application.Queries.GetInvoicesToStoreFromRegister;

namespace SovosCase.Application.Services
{
    public class JobService : Interfaces.IJobService
    {
        private readonly IMediator _mediator;
        private readonly IInvoiceSqlService _invoiceSqlService;
        private readonly ILogger<JobService> _logger;
        private readonly ISendEndpointProvider _sendEndpoint;

        public JobService(IMediator mediator, IInvoiceSqlService invoiceSqlService, ILogger<JobService> logger, ISendEndpointProvider sendEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _invoiceSqlService = invoiceSqlService ?? throw new ArgumentNullException(nameof(invoiceSqlService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sendEndpoint = sendEndpoint ?? throw new ArgumentNullException(nameof(sendEndpoint));
        }


        public async Task StoreInvoices()
        {
            var invoicesToStore = await _mediator.Send(new GetInvoicesToStoreFromRegisterQueryRequest());
            if (invoicesToStore == null)
            {
                _logger.LogInformation($"No Invoices found to Store.");
                return;
            }
            foreach (var invoice in invoicesToStore)
            {
                bool result = default;
                try
                {
                    result = await _invoiceSqlService.StoreInvoice(invoice);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Failed to StoreInvoice. Id: '{invoice.InvoiceHeader.InvoiceId}'. Exception: {ex.ToString()}");
                    if (ex.InnerException.Message.ToString().Contains("Cannot insert duplicate key")) // Failed to Store because the Invoice is already in Sql, but wasn't Updated in Mongo.
                        result = true;
                    else
                        continue;
                }
                if (result)
                {
                    await publishInvoiceStoreEvent(invoice.InvoiceHeader.InvoiceId);
                    BackgroundJob.Enqueue<IEmailService>(x => x.SendInvoiceInformationEmail(invoice.InvoiceHeader.InvoiceId));
                }
            }
        }

        private async Task publishInvoiceStoreEvent(string invoiceId)
        {
            try
            {
                var sendEndPoint = await _sendEndpoint.GetSendEndpoint(new Uri("queue:InvoiceStoreEvent"));
                var eventMessage = new InvoiceStoreEvent()
                {
                    InvoiceId = invoiceId,
                    IsStored = true
                };
                await sendEndPoint.Send<InvoiceStoreEvent>(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to Publish InvoiceStoreEvent. Id: '{invoiceId}'. Exception: {ex.ToString()}");
            }
        }
    }
}
