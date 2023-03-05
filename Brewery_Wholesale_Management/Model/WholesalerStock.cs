namespace Brewery_Wholesale_Management.Model
{
    public class WholesalerStock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }

        //public decimal PricePerUnit { get; set; }

    }
}
