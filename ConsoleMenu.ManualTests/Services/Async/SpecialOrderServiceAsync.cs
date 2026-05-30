using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Services.Async
{
    public class SpecialOrderServiceAsync : ISpecialOrderServiceAsync
    {
        private readonly ISpecialInventoryServiceAsync _specialInventoryServiceAsync;

        public SpecialOrderServiceAsync(ISpecialInventoryServiceAsync specialInventoryService)
        {
            _specialInventoryServiceAsync = specialInventoryService;
        }

        public async Task CreateOrderAllProductsAsync()
        {
            await _specialInventoryServiceAsync.ReserveAllSpecialItemsAsync();
            Console.WriteLine("Order for all special products successfully created.");
        }

        public async Task CreateOrderSpecificProductAsync(string productName)
        {
            await _specialInventoryServiceAsync.ReserveSpecificSpecialItemAsync(productName);
            Console.WriteLine($"Order for special product {productName} successfully created.");
        }
    }
}
