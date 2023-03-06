namespace Brewery_Wholesale_Management.Interfaces
{
    public interface IWholesalerService
    {
        Task<bool> UpdateBeerStock(int wholesalerId, int beerId, int Quantity);
    }
}
