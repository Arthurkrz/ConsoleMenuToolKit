using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.ManualTests.Contracts.Service.Async;

namespace ConsoleMenuToolKit.ManualTests.Handlers
{
    public class CreateOrderHandler : IConsoleMenuHandler
    {
        private readonly IOrderServiceAsync _orderService;

        public CreateOrderHandler(IOrderServiceAsync orderService)
        {
            _orderService = orderService;
        }

        public async Task ExecuteAsync() => await _orderService.CreateOrderAsync();

        public string Key => "create-order";
    }
}
