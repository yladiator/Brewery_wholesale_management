using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;

namespace Brewery_Wholesale_Management.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly BreweryDbContext _dbContext;

        public WholesalerService(BreweryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool UpdateBeerStock(int wholesalerId, int beerId, int Quantity)
        {
            var wholesalerStock = _dbContext.WholesalerStocks.FirstOrDefault(ws => ws.WholesalerId == wholesalerId && ws.BeerId == beerId);

            if (wholesalerStock == null)
            {
                throw new ArgumentException($"Wholesaler with ID {wholesalerId} does not have beer with ID {beerId} in their inventory.");
            }
            
            wholesalerStock.Quantity = Quantity;

            _dbContext.WholesalerStocks.Update(wholesalerStock);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
