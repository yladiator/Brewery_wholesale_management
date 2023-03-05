namespace Brewery_Wholesale_Management.Model
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WholesalerStock> WholesalerStocks { get; set; }
    }
}
