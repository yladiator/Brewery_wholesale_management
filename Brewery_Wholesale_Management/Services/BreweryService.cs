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

        public Beer AddBeer(BeerRequestModel request)
        {
            var beer = new Beer
            {
                Name = request.Name,
                AlcoholContent = request.AlcoholContent,
                Price = request.Price,
                BreweryId = request.BreweryId
            };
            var brewery = _dbContext.Breweries.FirstOrDefault(b => b.Id == beer.BreweryId);
            if (brewery == null)
            {
                return null;
            }

            _dbContext.Beers.Add(beer);
            _dbContext.SaveChanges();

            return beer;
        }

        public bool DeleteBeer(int beerId)
        {
            var beer = _dbContext.Beers.FirstOrDefault(b => b.Id == beerId);
            if (beer == null)
            {
                return false;
            }

            _dbContext.Beers.Remove(beer);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<Beer> GetBeersByBrewery(int breweryId)
        {
            return _dbContext.Beers.Include(b => b.Brewery)//Include(b=> b.Brewery?.Name) //.Include(b => b.Brewery)
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
          .ToList();
        }

        public WholesalerStock Add_UpdateWholesalerStock(int beerId, int wholesalerId, int quantity)
        {
            var beer = _dbContext.Beers.FirstOrDefault(b => b.Id == beerId);
            if (beer == null)
            {
                return null;
            }

            var wholesaler = _dbContext.Wholesalers.FirstOrDefault(w => w.Id == wholesalerId);
            if (wholesaler == null)
            {
                return null;
            }

            var wholesalerStock = _dbContext.WholesalerStocks
                .FirstOrDefault(ws => ws.BeerId == beerId && ws.WholesalerId == wholesalerId);

            if (wholesalerStock == null)
            {
                wholesalerStock = new WholesalerStock
                {
                    BeerId = beerId,
                    WholesalerId = wholesalerId,
                    Quantity = quantity
                };
                _dbContext.WholesalerStocks.Add(wholesalerStock);
            }
            else
            {
                wholesalerStock.Quantity = quantity;
                _dbContext.WholesalerStocks.Update(wholesalerStock);
            }

            _dbContext.SaveChanges();

            return wholesalerStock;
        }




    }
}
