using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementApi.Commands.Handlers;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Executors
{
    public class CommandExecutor : ICommandExecutor
    {
        private IEnumerable<IHandler> commandHandlers;

        public CommandExecutor(IEnumerable<IHandler> commandHandlers) => this.commandHandlers = commandHandlers;

        public Task ExecuteAsync(Command command) => commandHandlers
            .First(o => o.CommandType == command.GetType())
            .HandleCommandAsync(command);
    }
}