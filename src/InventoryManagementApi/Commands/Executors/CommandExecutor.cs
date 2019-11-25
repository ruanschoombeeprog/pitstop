using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementApi.Commands.Handlers;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Executors
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IEnumerable<IHandler> commandHandlers;

        public CommandExecutor(IEnumerable<IHandler> commandHandlers) => this.commandHandlers = commandHandlers;

        public Task ExecuteAsync(Command command) => commandHandlers
            .Where(o => o.CommandType == command.GetType())
            .Cast<dynamic>() // TODO: Better way of handling the casting
            .First()
            .HandleCommandAsync(command);

        public Task<TResponse> ExecuteAsync<TResponse>(Command command) => commandHandlers
            .Where(o => o.CommandType == command.GetType())
            .Cast<dynamic>() // TODO: Better way of handling the casting
            .First()
            .HandleCommandAsync(command);
    }
}