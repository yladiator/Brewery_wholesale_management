using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Microsoft.EntityFrameworkCore;

namespace Brewery_Wholesale_Management.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly BreweryDbContext _dbContext;

        public QuoteService(BreweryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public QuoteResponse RequestQuote(int wholesalerId, IEnumerable<QuoteRequestItem> quoteRequestItems)
        {
            if (quoteRequestItems == null || quoteRequestItems.Count() == 0)
            {
                throw new ArgumentException("Order cannot be empty");
            }

            var wholesaler = _dbContext.Wholesalers.FirstOrDefault(w => w.Id == wholesalerId);
            if (wholesaler == null)
            {
                throw new ArgumentException("Wholesaler does not exist");
            }

            var distinctBeers = quoteRequestItems.Select(q => q.BeerId).Distinct();
            if (distinctBeers.Count() != quoteRequestItems.Count())
            {
                throw new ArgumentException("There cannot be any duplicates in the order");
            }

            decimal totalPrice = 0.00m;
            var summary = new List<QuoteSummaryItem>();

            foreach (var item in quoteRequestItems)
            {
                var beer = _dbContext.Beers.Include(a => a.WholesalerStocks).FirstOrDefault(b => b.Id == item.BeerId);
                if (beer == null)
                {
                    throw new ArgumentException($"Beer with ID {item.BeerId} does not exist");
                }

                if (beer.WholesalerStocks.FirstOrDefault(b => b.WholesalerId == wholesalerId)?.WholesalerId != wholesalerId)
                {
                    throw new ArgumentException($"Beer with ID {item.BeerId} is not sold by the wholesaler with ID {wholesalerId}");
                }

                int quantity = item.Quantity;
                var price = beer.Price;

                var WholesalerStocks_ = wholesaler.WholesalerStocks.FirstOrDefault(s => s.BeerId == item.BeerId)?.Quantity ?? 0;

                if (quantity > WholesalerStocks_)
                {
                    throw new ArgumentException($"Wholesaler with ID {wholesalerId} does not have enough stock for beer with ID {item.BeerId}");
                }

                if (quantity > 20)
                {
                    price *= 0.8m;
                }
                else if (quantity > 10)
                {
                    price *= 0.9m;
                }

                totalPrice += price * quantity;
                summary.Add(new QuoteSummaryItem { BeerId = item.BeerId, BeerName= beer.Name, Quantity = quantity, Price = price });
            }

            return new QuoteResponse { TotalPrice = totalPrice, Summary = summary, Wholesaler = wholesaler };
        }
    }
}
