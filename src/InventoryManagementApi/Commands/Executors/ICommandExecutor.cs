using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Executors
{
    public interface ICommandExecutor
    { 
        Task ExecuteAsync(Command command);
    }
}