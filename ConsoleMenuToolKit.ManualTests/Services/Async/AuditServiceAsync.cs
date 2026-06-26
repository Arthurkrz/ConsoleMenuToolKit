using ConsoleMenuToolKit.ManualTests.Contracts.Service.Async;

namespace ConsoleMenuToolKit.ManualTests.Services.Async
{
    public class AuditServiceAsync : IAuditServiceAsync
    {
        public async Task RegisterAsync(string message)
        {
            await Task.Delay(1);
            Console.WriteLine($"Audit log: {message}");
        }
    }
}
