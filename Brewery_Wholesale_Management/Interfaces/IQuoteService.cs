using Brewery_Wholesale_Management.Model;

namespace Brewery_Wholesale_Management.Interfaces
{
    public interface IQuoteService
    {
        QuoteResponse RequestQuote(int wholesalerId, IEnumerable<QuoteRequestItem> items);
    }
}
