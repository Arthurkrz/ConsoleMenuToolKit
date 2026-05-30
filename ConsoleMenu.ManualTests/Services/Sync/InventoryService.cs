using ConsoleMenu.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenu.ManualTests.Services.Sync
{
    public class InventoryService : IInventoryService
    {
        public void ReserveItems() =>
            Console.WriteLine("Inventory checked and items reserved.");
    }
}
