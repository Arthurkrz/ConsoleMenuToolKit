using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Handlers
{
    public class CreateOrderSpecialItem1Handler : IConsoleMenuHandler
    {
        private readonly ISpecialOrderServiceAsync _specialOrderServiceAsync;

        public CreateOrderSpecialItem1Handler(ISpecialOrderServiceAsync specialOrderServiceAsync)
        {
            _specialOrderServiceAsync = specialOrderServiceAsync;
        }

        public async Task ExecuteAsync() => 
            await _specialOrderServiceAsync.CreateOrderSpecificProductAsync("Item 1");

        public string Key => "create-order-specific-item-1";
    }
}
