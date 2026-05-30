namespace ConsoleMenu.ManualTests.Contracts.Service.Async
{
    public interface ISpecialInventoryServiceAsync
    {
        Task ReserveAllSpecialItemsAsync();

        Task ReserveSpecificSpecialItemAsync(string itemName);
    }
}
