using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SovosCase.Application.Interfaces;
using SovosCase.Application.Models.Requests;

namespace SovosCase.WebAPI.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IInvoiceSqlService _invoiceSqlService;
        private readonly ILogger<StoreController> _logger;
        private readonly IMapper _mapper;

        public StoreController(IInvoiceSqlService invoiceSqlService, ILogger<StoreController> logger, IMapper mapper)
        {
            _invoiceSqlService = invoiceSqlService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet("addorupdatejobstoreinvoices")]
        public async Task<IActionResult> AddOrUpdateJobStoreInvoices(string cronExpression = "*/15 * * * *")
        {
            _logger.LogInformation($"AddOrUpdateJob: StoreInvoices Request received. CronTimer: {cronExpression}");
            Hangfire.RecurringJob.AddOrUpdate<IJobService>(job => job.StoreInvoices(), cronExpression);
            return Ok($"Successfully Added/Updated job: 'StoreInvoices'. CronTimer: {cronExpression}");
        }

        [HttpGet("getstoredinvoices")]
        public async Task<IActionResult> GetStoredInvoices()
        {
            _logger.LogInformation($"GetStoredInvoices Request received.");
            return Ok(await _invoiceSqlService.GetAllInvoices());
        }

        [HttpGet("getstoredinvoicebyid/{id}")]
        public async Task<IActionResult> GetStoredInvoiceById(Guid id)
        {
            _logger.LogInformation($"GetStoredInvoiceById Request received: {id}");
            return Ok(await _invoiceSqlService.GetInvoiceById(id));
        }

        [HttpGet("getstoredinvoicebyinvoiceid/{invoiceid}")]
        public async Task<IActionResult> GetStoredInvoiceByInvoiceId(string invoiceid)
        {
            _logger.LogInformation($"GetStoredInvoiceByInvoiceId Request received: {invoiceid}");
            return Ok(await _invoiceSqlService.GetInvoiceByInvoiceId(invoiceid));
        }

        [HttpGet("getstoredinvoiceslist")]
        public async Task<IActionResult> GetStoredInvoicesList([FromQuery] GetInvoiceListSqlRequest getInvoiceListRequest)
        {
            _logger.LogInformation($"GetStoredInvoicesList Request received: {getInvoiceListRequest}");
            return Ok(await _invoiceSqlService.GetInvoiceList(getInvoiceListRequest));
        }

        [HttpGet("automappercontrol")]
        public async Task<IActionResult> AutoMapperControl()
        {
            try
            {
                _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
            return Ok();
        }
    }
}
