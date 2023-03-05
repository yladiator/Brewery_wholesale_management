namespace Brewery_Wholesale_Management.Model
{
    public class QuoteResponse
    {
        public decimal TotalPrice { get; set; }
        public List<QuoteSummaryItem> Summary { get; set; }

        public Wholesaler Wholesaler { get; set; }
    }
}
