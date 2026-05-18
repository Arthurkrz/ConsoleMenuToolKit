using ConsoleMenu.ManualTests.Contracts.Service;

namespace ConsoleMenu.ManualTests.Services
{
    public class InventoryService : IInventoryService
    {
        public async Task ReserveItemsAsync()
        {
            await Task.Delay(1);
            Console.WriteLine("Inventory checked and items reserved.");
        }
    }
}
