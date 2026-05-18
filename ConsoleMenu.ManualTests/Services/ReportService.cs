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
    }
}
