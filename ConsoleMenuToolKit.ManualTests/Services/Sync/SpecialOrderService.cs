using ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenuToolKit.ManualTests.Services.Sync
{
    public class SpecialOrderService : ISpecialOrderService
    {
        private readonly ISpecialInventoryService _specialInventoryService;

        public SpecialOrderService(ISpecialInventoryService specialInventoryService)
        {
            _specialInventoryService = specialInventoryService;
        }

        public void CreateOrderAllProducts()
        {
            _specialInventoryService.ReserveAllSpecialItems();
            Console.WriteLine("Order for all special products successfully created.");
        }

        public void CreateOrderSpecificProduct(string productName)
        {
            _specialInventoryService.ReserveSpecificSpecialItem(productName);
            Console.WriteLine($"Order for special product {productName} successfully created.");
        }
    }
}
