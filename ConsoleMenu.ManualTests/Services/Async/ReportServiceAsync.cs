using ConsoleMenu.ManualTests.Contracts.Service.Async;

namespace ConsoleMenu.ManualTests.Services.Async
{
    public class ReportServiceAsync : IReportServiceAsync
    {
        private readonly IAuditServiceAsync _auditServiceAsync;

        public ReportServiceAsync(IAuditServiceAsync auditService)
        {
            _auditServiceAsync = auditService;
        }

        public async Task GenerateDailyReportAsync()
        {
            await _auditServiceAsync.RegisterAsync("Daily report requested.");
            Console.WriteLine("Daily report generated.");
        }

        public async Task GenerateReportForAllSpecialItemsAsync()
        {
            await _auditServiceAsync.RegisterAsync("Report for all special items requested.");
            Console.WriteLine("Report for all special items generated.");
        }
    }
}
