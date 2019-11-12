using Microsoft.AspNetCore.Mvc;
using Moq;
using Pitstop.WorkshopManagementAPI.Controllers;
using Pitstop.WorkshopManagementAPI.Domain;
using Pitstop.WorkshopManagementAPI.Repositories;
using System;
using TechTalk.SpecFlow;
using WorkshopManagement.AcceptanceTests.Context;
using WorkshopManagementAPI.CommandHandlers;
using Xunit;

namespace WorkshopManagement.AcceptanceTests
{
    [Binding]
    public class WorkshopPlanningSteps
    {
        private readonly IWorkshopPlanningRepository planningRepo;
        private readonly IPlanMaintenanceJobCommandHandler planMaintenanceJobCommandHandler;
        private readonly IFinishMaintenanceJobCommandHandler finishMaintenanceJobCommandHandler;
        private readonly WorkshopPlanningContext workshopPlanningContext;
        private WorkshopPlanningController workshopPlanningController;

        public WorkshopPlanningSteps(IWorkshopPlanningRepository planningRepo,
            IPlanMaintenanceJobCommandHandler planMaintenanceJobCommandHandler,
            IFinishMaintenanceJobCommandHandler finishMaintenanceJobCommandHandler,
            WorkshopPlanningContext workshopPlanningContext)
        {
            this.planningRepo = planningRepo;
            this.planMaintenanceJobCommandHandler = planMaintenanceJobCommandHandler;
            this.finishMaintenanceJobCommandHandler = finishMaintenanceJobCommandHandler;
            this.workshopPlanningContext = workshopPlanningContext;
        }

        [Given(@"the user has selected the Workshop Management menu item")]
        public void GivenTheUserHasSelectedTheWorkshopManagementMenuItem()
        {
            this.workshopPlanningController = new WorkshopPlanningController(planningRepo, planMaintenanceJobCommandHandler, finishMaintenanceJobCommandHandler);
        }

        [Given(@"todays date is the following")]
        public void GivenTodaysDateIsTheFollowing(Table table)
        {
            if (table.Rows[0].TryGetValue("", out var value))
                if (DateTime.TryParse(value, out var planngingDate))
                    workshopPlanningContext.PlanningDate = planngingDate;
        }

        [When(@"the WebApp loads the Webshop management page")]
        public async System.Threading.Tasks.Task WhenTheWebAppLoadsTheWebshopManagementPageAsync()
        {
            var planning = await workshopPlanningController.GetByDate(workshopPlanningContext.PlanningDate);
            workshopPlanningContext.ActionResult = planning;
        }

        [Then(@"the WebApp should display (.*) job")]
        public void ThenTheWebAppShouldDisplayJob(int p0)
        {
            if (workshopPlanningContext.ActionResult is OkObjectResult okObjectResult)
                if (okObjectResult.Value is WorkshopPlanning planning)
                    Assert.True(planning.Jobs.Count == p0);
        }
    }
}
