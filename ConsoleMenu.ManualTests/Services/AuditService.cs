using ConsoleMenu.ManualTests.Contracts.Service;

namespace ConsoleMenu.ManualTests.Services
{
    public class AuditService : IAuditService
    {
        public async Task RegisterAsync(string message)
        {
            await Task.Delay(1);
            Console.WriteLine($"Audit log: {message}");
        }
    }
}
