namespace ConsoleMenu.ManualTests.Contracts.Service
{
    public interface IAuditService
    {
        Task RegisterAsync(string message);
    }
}
