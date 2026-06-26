using ConsoleMenuToolKit.ManualTests.Contracts.Service.Sync;

namespace ConsoleMenuToolKit.ManualTests.Services.Sync
{
    public class AuditService : IAuditService
    {
        public void Register(string message) =>
            Console.WriteLine($"Audit log: {message}");
    }
}
