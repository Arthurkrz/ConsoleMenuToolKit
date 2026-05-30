using ConsoleMenu.Contracts;
using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Handlers
{
    public class GenerateDailyReportHandler : IConsoleMenuHandler
    {
        private readonly IReportServiceAsync _reportService;

        public GenerateDailyReportHandler(IReportServiceAsync reportService)
        {
            _reportService = reportService;
        }

        public async Task ExecuteAsync() => await _reportService.GenerateDailyReportAsync();

        public string Key => "generate-daily-report";
    }
}
