using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class GetAllInventoryHandler : ICommandHandler<GetAllInventory, IEnumerable<Inventory>>
    {
        private readonly IInventoryRepository repository;

        public GetAllInventoryHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public Type CommandType => typeof(GetAllInventory);

        public Task<IEnumerable<Inventory>> HandleCommandAsync(Command command) => HandleCommandAsync((GetAllInventory)command);

        private async Task<IEnumerable<Inventory>> HandleCommandAsync(GetAllInventory command)
        {
            var result = await repository.GetAllAsync();

            Console.WriteLine($"Command Handled : {command.GetType().Name}");

            return result;
        }
    }
}