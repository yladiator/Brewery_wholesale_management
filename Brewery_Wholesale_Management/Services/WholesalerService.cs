using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Microsoft.EntityFrameworkCore;

namespace Brewery_Wholesale_Management.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly BreweryDbContext _dbContext;

        public WholesalerService(BreweryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UpdateBeerStock(int wholesalerId, int beerId, int Quantity)
        {
            var wholesalerStock = await  _dbContext.WholesalerStocks.FirstOrDefaultAsync(ws => ws.WholesalerId == wholesalerId && ws.BeerId == beerId);

            if (wholesalerStock == null)
            {
                throw new ArgumentException($"Wholesaler with ID {wholesalerId} does not have beer with ID {beerId} in their inventory.");
            }
            
            wholesalerStock.Quantity = Quantity;

            _dbContext.WholesalerStocks.Update(wholesalerStock);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
