using ConsoleMenu.ManualTests.Contracts.Service;

namespace ConsoleMenu.ManualTests.Services
{
    public class SpecialInventoryService : ISpecialInventoryService
    {
        public async Task ReserveAllSpecialItemsAsync()
        {
            await Task.Delay(1);
            Console.WriteLine("Inventory checked and all special items reserved.");
        }

        public async Task ReserveSpecificSpecialItemAsync(string itemName)
        {
            await Task.Delay(1);
            Console.WriteLine("Inventory checked and specific special item reserved: " + itemName);
        }
    }
}
