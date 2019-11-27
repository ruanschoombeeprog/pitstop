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
    public class RegisterInventoryHandler : ICommandHandler<RegisterInventory>
    {
        private readonly IInventoryRepository repository;
        private readonly IMessagePublisher messagePublisher;

        public RegisterInventoryHandler(IInventoryRepository repository, IMessagePublisher messagePublisher)
        {
            this.repository = repository;
            this.messagePublisher = messagePublisher;
        }

        public Type CommandType => typeof(RegisterInventory);

        public Task HandleCommandAsync(Command command) => HandleCommandAsync((RegisterInventory)command);

        private async Task HandleCommandAsync(RegisterInventory command)
        {
            command.InventoryPriceShouldBeZerorOrLarger();
            command.InventoryProductCodeLengthShouldBe12CharactersLong();
            command.InventoryQuantityShouldBeZeroOrLarger();

            await repository.InsertAsync(command.ToModel());

            await messagePublisher.publishEventAsync(command.ToEvent());

            Console.WriteLine($"Command Handled : {command.GetType().Name}");
        }
    }
}