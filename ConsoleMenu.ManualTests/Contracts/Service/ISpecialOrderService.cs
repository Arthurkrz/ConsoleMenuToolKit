namespace ConsoleMenu.ManualTests.Contracts.Service
{
    public interface ISpecialOrderService
    {
        Task CreateOrderAllProductsAsync();

        Task CreateOrderSpecificProductAsync(string productName);
    }
}
