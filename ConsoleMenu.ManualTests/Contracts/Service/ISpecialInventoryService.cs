namespace ConsoleMenu.ManualTests.Contracts.Service
{
    public interface ISpecialInventoryService
    {
        Task ReserveAllSpecialItemsAsync();

        Task ReserveSpecificSpecialItemAsync(string itemName);
    }
}
