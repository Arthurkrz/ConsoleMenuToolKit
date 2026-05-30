namespace ConsoleMenu.ManualTests.Contracts.Service.Async
{
    public interface IReportServiceAsync
    {
        Task GenerateDailyReportAsync();

        Task GenerateReportForAllSpecialItemsAsync();
    }
}
