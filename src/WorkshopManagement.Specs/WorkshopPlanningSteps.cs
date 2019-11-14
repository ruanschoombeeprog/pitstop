using Pitstop.WorkshopManagementAPI.Domain;
using System;
using TechTalk.SpecFlow;
using WorkshopManagement.UnitTests.TestdataBuilders;
using Xunit;

namespace Pitstop.WorkshopManagement.Specs
{
    [Binding]
    public class WorkshopPlanningSteps
    {
        private WorkshopPlanning workshopPlanning;

        [Given(@"I have a new workshop planning for (.*)")]
        public void GivenIHaveANewWorkshopPlanning(DateTime date)
        {
            workshopPlanning = new WorkshopPlanning(date);
        }

        [When(@"I plan a maintenance job")]
        public void WhenIPlanAMaintenanceJob()
        {
            var command = new PlanMaintenanceJobCommandBuilder().Build();
            workshopPlanning.PlanMaintenanceJob(command);
        }

        [Then(@"I should have one job in my planning")]
        public void ThenTheJobShouldFallWithinBusinessDay()
        {
            Assert.NotEmpty(workshopPlanning.Jobs);
        }

        [Then(@"I should have no job in my planning")]
        public void ThenIShouldHaveNoJobInMyPlanning()
        {
            Assert.Empty(workshopPlanning.Jobs);
        }
    }
}
