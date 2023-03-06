using Brewery_Wholesale_Management.Interfaces;
using Brewery_Wholesale_Management.Model;
using Microsoft.EntityFrameworkCore;

namespace Brewery_Wholesale_Management.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly BreweryDbContext _dbContext;
        public BreweryService(BreweryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Beer> AddBeer(BeerRequestModel request)
        {
            var beer = new Beer
            {
                Name = request.Name,
                AlcoholContent = request.AlcoholContent,
                Price = request.Price,
                BreweryId = request.BreweryId
            };
            var brewery = await _dbContext.Breweries.FirstOrDefaultAsync(b => b.Id == beer.BreweryId);
            if (brewery == null)
            {
                return null;
            }

            await _dbContext.Beers.AddAsync(beer);
            await _dbContext.SaveChangesAsync();

            return beer;
        }


        public async Task<bool> DeleteBeer(int beerId)
        {
            var beer = await  _dbContext.Beers.FirstOrDefaultAsync(b => b.Id == beerId);
            if (beer == null)
            {
                return false;
            }

            _dbContext.Beers.Remove(beer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId)
        {
            return await  _dbContext.Beers.Include(b => b.Brewery)//Include(b=> b.Brewery?.Name) //.Include(b => b.Brewery)
          .Include(b => b.WholesalerStocks)
          .Where(b => b.BreweryId == breweryId)
          .Select(b => new Beer
          {
              Id = b.Id,
              Name = b.Name,
              AlcoholContent = b.AlcoholContent,
              Price = b.Price,
              BreweryId = b.BreweryId,
              Brewery = b.Brewery != null ? new Brewery
              {
                  Id = b.Brewery.Id,
                  Name = b.Brewery.Name
              } : null,
              WholesalerStocks = b.WholesalerStocks
          })
          .ToListAsync();
        }

        public async Task<WholesalerStock> Add_UpdateWholesalerStock(int beerId, int wholesalerId, int quantity)
        {
            var beer = await _dbContext.Beers.FirstOrDefaultAsync(b => b.Id == beerId);
            if (beer == null)
            {
                return null;
            }

            var wholesaler = await  _dbContext.Wholesalers.FirstOrDefaultAsync(w => w.Id == wholesalerId);
            if (wholesaler == null)
            {
                return null;
            }

            var wholesalerStock = await  _dbContext.WholesalerStocks
                .FirstOrDefaultAsync(ws => ws.BeerId == beerId && ws.WholesalerId == wholesalerId);

            if (wholesalerStock == null)
            {
                wholesalerStock = new WholesalerStock
                {
                    BeerId = beerId,
                    WholesalerId = wholesalerId,
                    Quantity = quantity
                };
                await _dbContext.WholesalerStocks.AddAsync(wholesalerStock);
            }
            else
            {
                wholesalerStock.Quantity = quantity;
                 _dbContext.WholesalerStocks.Update(wholesalerStock);
            }

            await _dbContext.SaveChangesAsync();

            return wholesalerStock;
        }
    }
}
