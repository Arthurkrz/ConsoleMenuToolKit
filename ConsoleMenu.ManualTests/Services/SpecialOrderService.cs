using ConsoleMenu.ManualTests.Contracts.Service;

namespace ConsoleMenu.ManualTests.Services
{
    public class SpecialOrderService : ISpecialOrderService
    {
        private readonly ISpecialInventoryService _specialInventoryService;

        public SpecialOrderService(ISpecialInventoryService specialInventoryService)
        {
            _specialInventoryService = specialInventoryService;
        }

        public async Task CreateOrderAllProductsAsync()
        {
            await _specialInventoryService.ReserveAllSpecialItemsAsync();
            Console.WriteLine("Order for all special products successfully created.");
        }

        public Task CreateOrderSpecificProductAsync(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
