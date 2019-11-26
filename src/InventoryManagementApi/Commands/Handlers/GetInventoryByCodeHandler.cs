using System;
using System.Threading.Tasks;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class GetInventoryByCodeHandler : IHandler<GetInventoryByCode, Inventory>
    {
        private readonly IInventoryRepository repository;

        public GetInventoryByCodeHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }
        public Type CommandType => typeof(GetInventoryByCode);

        public Task<Inventory> HandleCommandAsync(Command command) => HandleCommandAsync((GetInventoryByCode)command);

        private async Task<Inventory> HandleCommandAsync(GetInventoryByCode command)
        {
            var result = await repository.GetItemByProductCode(command.ProductCode);

            Console.WriteLine($"Command Handled : {command.GetType().Name}");

            return result;
        }
    }
}