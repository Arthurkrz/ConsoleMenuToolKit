using ConsoleMenuToolKit.ManualTests.Contracts.Service.Async;

namespace ConsoleMenuToolKit.ManualTests.Services.Async
{
    public class InventoryServiceAsync : IInventoryServiceAsync
    {
        public async Task ReserveItemsAsync()
        {
            await Task.Delay(1);
            Console.WriteLine("Inventory checked and items reserved.");
        }
    }
}
