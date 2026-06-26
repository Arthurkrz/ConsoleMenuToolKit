using ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenuToolKit.ManualTests.Services.Sync
{
    public class ReportService : IReportService
    {
        private readonly IAuditService _auditService;

        public ReportService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public void GenerateDailyReport()
        {
            _auditService.Register("Daily report requested.");
            Console.WriteLine("Daily report generated.");
        }

        public void GenerateReportForAllSpecialItems()
        {
            _auditService.Register("Report for all special items requested.");
            Console.WriteLine("Report for all special items generated.");
        }
    }
}
