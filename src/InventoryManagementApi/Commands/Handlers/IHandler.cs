
using System;
using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public interface IHandler
    {
        Type CommandType { get; }
    }

    public interface IHandler<T> : IHandler where T : Command
    {
        Task HandleCommandAsync(Command command);
    }

    public interface IHandler<T, U> : IHandler where T : Command
    {
        Task<U> HandleCommandAsync(Command command);
    }
}