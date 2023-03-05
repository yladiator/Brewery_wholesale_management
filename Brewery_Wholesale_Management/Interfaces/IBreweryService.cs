using Brewery_Wholesale_Management.Model;

namespace Brewery_Wholesale_Management.Interfaces
{
    public interface IBreweryService
    {
        // List all the beers by brewery
        IEnumerable<Beer> GetBeersByBrewery(int breweryId);

        // Add a new beer
        Beer AddBeer(BeerRequestModel request);

        // Delete a beer
        bool DeleteBeer(int beerId);

        WholesalerStock Add_UpdateWholesalerStock(int beerId, int wholesalerId, int quantity);


    }
}
