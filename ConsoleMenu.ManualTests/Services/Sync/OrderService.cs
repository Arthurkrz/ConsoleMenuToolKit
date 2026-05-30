using ConsoleMenu.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenu.ManualTests.Services.Sync
{
    public class OrderService : IOrderService
    {
        private readonly IInventoryService _inventoryService;

        public OrderService(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void CreateOrder()
        {
            _inventoryService.ReserveItems();
            Console.WriteLine("Order successfully created.");
        }
    }
}
