namespace ConsoleMenu.ManualTests.Contracts.Service.Async
{
    public interface ISpecialOrderServiceAsync
    {
        Task CreateOrderAllProductsAsync();

        Task CreateOrderSpecificProductAsync(string productName);
    }
}
