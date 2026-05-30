namespace ConsoleMenu.ManualTests.Contracts.Service.Sync
{
    public interface IReportService
    {
        void GenerateDailyReport();

        void GenerateReportForAllSpecialItems();
    }
}
