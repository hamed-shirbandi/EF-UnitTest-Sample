namespace StoreManagement.Application.Prices
{
    public interface IPriceService
    {
        int GetHigherPrice(int price1, int price2);
    }
}