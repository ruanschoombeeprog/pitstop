using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementApi.Commands.Handlers;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Executors
{
    /// <summary>
    /// This class provides the ability to execute commands registered at startup.
    /// </summary>
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IEnumerable<IHandler> commandHandlers;

        public CommandExecutor(IEnumerable<IHandler> commandHandlers) => this.commandHandlers = commandHandlers;

        /// <summary>
        /// This method matches a command type to a command handler and executes the command returning no result. 
        /// </summary>
        /// <param name="command">The command being executed</param>
        /// <returns>Task</returns>
        public Task ExecuteAsync(Command command) => commandHandlers
            .Where(o => o.CommandType == command.GetType())
            .Cast<dynamic>() // TODO: Better way of handling the casting
            .First()
            .HandleCommandAsync(command);

        /// <summary> 
        /// This method matches a command type to a command handler and executes the command returning a response of type TResult. 
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="command">The command being executed</param>
        /// <returns></returns>
        public Task<TResponse> ExecuteAsync<TResponse>(Command command) => commandHandlers
            .Where(o => o.CommandType == command.GetType())
            .Cast<dynamic>() // TODO: Better way of handling the casting
            .First()
            .HandleCommandAsync(command);
    }
}