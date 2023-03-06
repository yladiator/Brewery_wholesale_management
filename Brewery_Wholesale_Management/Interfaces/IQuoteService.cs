using Brewery_Wholesale_Management.Model;

namespace Brewery_Wholesale_Management.Interfaces
{
    public interface IQuoteService
    {
         Task<QuoteResponse> RequestQuote(int wholesalerId, IEnumerable<QuoteRequestItem> items);
    }
}
