using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Brewery_Wholesale_Management.Model
{
    public class BeerRequestModel
    {
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }
    }

  }
