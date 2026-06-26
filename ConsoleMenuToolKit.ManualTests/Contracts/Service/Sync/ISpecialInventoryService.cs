namespace ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync
{
    public interface ISpecialInventoryService
    {
        void ReserveAllSpecialItems();

        void ReserveSpecificSpecialItem(string itemName);
    }
}
