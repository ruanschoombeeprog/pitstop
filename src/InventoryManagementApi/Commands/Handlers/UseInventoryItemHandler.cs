using InventoryManagementApi.Repositories;
using Pitstop.Infrastructure.Messaging;
using Pitstop.InventoryManagementApi.Domain.Rules;
using Pitstop.InventoryManagementApi.Events;
using Pitstop.InventoryManagementApi.Events.Extensions;
using Pitstop.InventoryManagementApi.Models.Extensions;
using System;
using System.Threading.Tasks;

namespace InventoryManagementApi.Commands.Handlers
{
    public class UseInventoryItemHandler : ICommandHandler<UseInventoryItem>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public UseInventoryItemHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }
        
        public Type CommandType => typeof(UseInventoryItem);

        public Task HandleCommandAsync(Command command)
        {
            return HandleUseInventoryItemAsync((UseInventoryItem)command);
        }

        private async Task HandleUseInventoryItemAsync(UseInventoryItem command)
        {
            var inventory = await repository.GetItemByProductCodeAsync(command.ProductCode);

            command.UseInventoryQuantityMustBeSmallerOrEqualToInventoryLevel(inventory);

            // Update Inventory

            inventory.Quantity -= command.Quantity;

            await repository.UpdateAsync(inventory);

            // Update InventoryUsed

            var totalPrice = command.Quantity * inventory.UnitPrice;

            var useInventoryItem = command.ToModel(totalPrice);

            await repository.UseInventoryAsync(useInventoryItem);

            // Emit event

            await messagePublisher.publishEventAsync(command.ToEvent());

            Console.WriteLine($"Command Handled : {command.GetType().Name}");
        }
    }
}