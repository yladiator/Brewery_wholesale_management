using System.Collections.Generic;
using System.Reflection.Emit;

namespace Brewery_Wholesale_Management.Model
{
    public class Beer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }
        public Brewery? Brewery { get; set; }
        public List<WholesalerStock>? WholesalerStocks { get; set; }
    }
}
