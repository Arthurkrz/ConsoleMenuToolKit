using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Services.Async
{
    public class OrderServiceAsync : IOrderServiceAsync
    {
        private readonly IInventoryServiceAsync _inventoryServiceAsync;

        public OrderServiceAsync(IInventoryServiceAsync inventoryService)
        {
            _inventoryServiceAsync = inventoryService;
        }

        public async Task CreateOrderAsync()
        {
            await _inventoryServiceAsync.ReserveItemsAsync();
            Console.WriteLine("Order successfully created.");
        }
    }
}
