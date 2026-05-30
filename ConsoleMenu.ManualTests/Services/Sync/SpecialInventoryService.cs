using ConsoleMenu.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenu.ManualTests.Services.Sync
{
    public class SpecialInventoryService : ISpecialInventoryService
    {
        public void ReserveAllSpecialItems() =>
            Console.WriteLine("Inventory checked and all special items reserved.");

        public void ReserveSpecificSpecialItem(string itemName) =>
            Console.WriteLine("Inventory checked and specific special item reserved: " + itemName);
    }
}
