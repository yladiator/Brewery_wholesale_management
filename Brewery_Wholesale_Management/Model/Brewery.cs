namespace Brewery_Wholesale_Management.Model
{
    public class Brewery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
