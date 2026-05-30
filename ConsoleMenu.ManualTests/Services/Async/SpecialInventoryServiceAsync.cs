using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Services.Async
{
    public class SpecialInventoryServiceAsync : ISpecialInventoryServiceAsync
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
