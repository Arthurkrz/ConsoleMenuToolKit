using ConsoleMenu.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenu.ManualTests.Services.Sync
{
    public class AuditService : IAuditService
    {
        public void Register(string message) =>
            Console.WriteLine($"Audit log: {message}");
    }
}
