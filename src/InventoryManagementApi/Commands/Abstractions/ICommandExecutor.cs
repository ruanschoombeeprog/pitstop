using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Executors
{
    public interface ICommandExecutor
    { 
        Task RunAsync(Command command);
        Task<TResult> RunAsync<TResult>(Command command);
    }
}