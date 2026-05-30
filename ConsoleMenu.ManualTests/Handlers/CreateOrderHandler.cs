using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Handlers
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
