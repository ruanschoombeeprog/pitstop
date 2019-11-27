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
    public class UpdateInventoryHandler : ICommandHandler<UpdateInventory>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public UpdateInventoryHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }
        
        public Type CommandType => typeof(UpdateInventory);

        public Task HandleCommandAsync(Command command) => HandleCommandAsync((UpdateInventory)command);

        private async Task HandleCommandAsync(UpdateInventory command)
        {
            command.InventoryPriceShouldBeZerorOrLarger();
            command.InventoryProductCodeLengthShouldBe12CharactersLong();
            command.InventoryQuantityShouldBeZeroOrLarger();

            await repository.UpdateAsync(command.ToModel());

            await messagePublisher.publishEventAsync(command.ToEvent());

            Console.WriteLine($"Command Handled : {command.GetType().Name}");
        }
    }
}