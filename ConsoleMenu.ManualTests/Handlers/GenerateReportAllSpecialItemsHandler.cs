using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Handlers
{
    public class GenerateReportAllSpecialItemsHandler : IConsoleMenuHandler
    {
        private readonly IReportServiceAsync _reportServiceAsync;

        public GenerateReportAllSpecialItemsHandler(IReportServiceAsync reportServiceAsync)
        {
            _reportServiceAsync = reportServiceAsync;
        }

        public async Task ExecuteAsync() =>
            await _reportServiceAsync.GenerateReportForAllSpecialItemsAsync();

        public string Key => "generate-report-all-special-items";
    }
}
