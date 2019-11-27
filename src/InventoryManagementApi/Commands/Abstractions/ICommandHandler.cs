
using System;
using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
    }

    public interface ICommandHandler<T> : ICommandHandler where T : Command
    {
        Task HandleCommandAsync(Command command);
    }

    public interface ICommandHandler<T, U> : ICommandHandler where T : Command
    {
        Task<U> HandleCommandAsync(Command command);
    }
}