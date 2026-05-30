using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Handlers
{
    public class CreateOrderSpecialItem2Handler : IConsoleMenuHandler
    {
        private readonly ISpecialOrderServiceAsync _specialOrderServiceAsync;

        public CreateOrderSpecialItem2Handler(ISpecialOrderServiceAsync specialOrderServiceAsync)
        {
            _specialOrderServiceAsync = specialOrderServiceAsync;
        }

        public async Task ExecuteAsync() =>
            await _specialOrderServiceAsync.CreateOrderSpecificProductAsync("Item 2");

        public string Key => "create-order-specific-item-2";
    }
}
