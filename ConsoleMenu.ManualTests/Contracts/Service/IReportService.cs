namespace ConsoleMenu.ManualTests.Contracts.Service
{
    public interface IReportService
    {
        Task GenerateDailyReportAsync();

        Task GenerateReportForAllSpecialItemsAsync();
    }
}
