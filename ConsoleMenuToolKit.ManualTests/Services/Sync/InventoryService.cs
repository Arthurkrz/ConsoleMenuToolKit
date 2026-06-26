using ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenuToolKit.ManualTests.Services.Sync
{
    public class InventoryService : IInventoryService
    {
        public void ReserveItems() =>
            Console.WriteLine("Inventory checked and items reserved.");
    }
}
