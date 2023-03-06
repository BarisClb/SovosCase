using SovosCase.Application.Models.Enums;
using SovosCase.Application.Responses;

namespace SovosCase.Application.Models.Requests
{
    public class GetInvoiceListSqlRequest : SortedListRequest
    {
        public InvoiceSearchInType? SearchIn { get; set; }
        public InvoiceOrderByType? OrderBy { get; set; }
        public List<string>? Includes { get; set; }
    }
}
