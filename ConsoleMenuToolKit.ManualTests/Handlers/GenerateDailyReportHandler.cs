using ConsoleMenuToolKit.Contracts;
using ConsoleMenuToolKit.ManualTests.Contracts.Service.Async;

namespace ConsoleMenuToolKit.ManualTests.Handlers
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
