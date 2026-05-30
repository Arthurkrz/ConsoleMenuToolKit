namespace ConsoleMenu.ManualTests.Contracts.Service.Sync
{
    public interface ISpecialOrderService
    {
        void CreateOrderAllProducts();

        void CreateOrderSpecificProduct(string productName);
    }
}
