using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagementApi.Models;
using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;

namespace InventoryManagementApi.Commands.Handlers
{
    public class GetAllInventoryHandler : IHandler<GetAllInventory, IEnumerable<Inventory>>
    {
        private readonly IInventoryRepository repository;

        public GetAllInventoryHandler(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public Type CommandType => typeof(GetAllInventory);

        public Task<IEnumerable<Inventory>> HandleCommandAsync(Command command)
        {
            return HandleGetAllInventoryAsync((GetAllInventory) command);
        }

        private Task<IEnumerable<Inventory>> HandleGetAllInventoryAsync(GetAllInventory command)
        {
            Console.WriteLine($"Command Handled : {command.GetType().Name}");
            return repository.GetAll();
        }
    }
}