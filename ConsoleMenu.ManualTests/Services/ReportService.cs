using ConsoleMenu.ManualTests.Contracts.Service;

namespace ConsoleMenu.ManualTests.Services
{
    public class ReportService : IReportService
    {
        private readonly IAuditService _auditService;

        public ReportService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public async Task GenerateDailyReportAsync()
        {
            await _auditService.RegisterAsync("Daily report requested.");
            Console.WriteLine("Daily report generated.");
        }

        public async Task GenerateReportForAllSpecialItemsAsync()
        {
            await _auditService.RegisterAsync("Report for all special items requested.");
            Console.WriteLine("Report for all special items generated.");
        }
    }
}
