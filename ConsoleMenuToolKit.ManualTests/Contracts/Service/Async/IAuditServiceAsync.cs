namespace ConsoleMenuToolKit.ManualTests.Contracts.Service.Async
{
    public interface IAuditServiceAsync
    {
        Task RegisterAsync(string message);
    }
}
