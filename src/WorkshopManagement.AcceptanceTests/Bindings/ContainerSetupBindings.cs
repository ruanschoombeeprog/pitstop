using BoDi;
using Moq;
using Pitstop.Infrastructure.Messaging;
using Pitstop.WorkshopManagementAPI.Domain;
using Pitstop.WorkshopManagementAPI.Repositories;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using WorkshopManagementAPI.CommandHandlers;

namespace WorkshopManagement.AcceptanceTests.Bindings
{
    [Binding]
    public class ContainerSetupBindings
    {
        private readonly IObjectContainer container;
        public ContainerSetupBindings(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void SetupContainerBindings()
        {
            var workshopMockRepo = new Mock<IWorkshopPlanningRepository>();

            workshopMockRepo
                .Setup(x => x.GetWorkshopPlanningAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(It.IsAny<WorkshopPlanning>());

            workshopMockRepo.Setup(x =>
                x.SaveWorkshopPlanningAsync(It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<IEnumerable<Event>>()));

            var planCommandHandlerMock = new Mock<IPlanMaintenanceJobCommandHandler>();

            var finishCommandHandlerMock = new Mock<IFinishMaintenanceJobCommandHandler>();

            container.RegisterInstanceAs(workshopMockRepo.Object);
            container.RegisterInstanceAs(planCommandHandlerMock.Object);
            container.RegisterInstanceAs(finishCommandHandlerMock.Object);
        }
    }
}
