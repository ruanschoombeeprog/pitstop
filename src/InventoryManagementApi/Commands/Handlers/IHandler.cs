
using System;
using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public interface IHandler
    {
        Type CommandType { get; }
        Task HandleCommandAsync(Command command);
    }

    public interface IHandler<T> : IHandler where T : Command { }
}