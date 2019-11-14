using Pitstop.WorkshopManagementAPI.Domain;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using WorkshopManagement.UnitTests.TestdataBuilders;
using Xunit;

namespace Pitstop.WorkshopManagement.Specs
{
    [Binding]
    public class WorkshopPlanningSteps
    {
        private readonly WorkshopPlanningContext context;

        public WorkshopPlanningSteps(WorkshopPlanningContext context)
        {
            this.context = context;
        }

        [Given(@"I have a new workshop planning for (.*)")]
        public void GivenIHaveANewWorkshopPlanning(DateTime date)
        {
            if (context.WorkshopPlanning == null)
                context.WorkshopPlanning = WorkshopPlanning.Create(date);
        }

        [Given(@"the (.*) and (.*) is set for vehicle (.*)")]
        public void GivenStartTimeAndTheEndTimeIsSet(DateTime startTime, DateTime endTime, string vehicleId)
        {
            context.VehicleId = vehicleId;
            context.StartTime = startTime;
            context.EndTime = endTime;
        }

        [When(@"I plan a maintenance job")]
        public void WhenIPlanAMaintenanceJob()
        {
            var customer = context.CustomerBuilder
                .Build();

            var vehicle = context.VehicleBuilder
                .WithOwnerId(customer.Id)
                .WithLicenseNumber(context.VehicleId)
                .Build();

            context.MaintenanceJobBuilder = context.MaintenanceJobBuilder
                .WithCustomer(customer)
                .WithVehicle(vehicle)
                .WithStartTime(context.StartTime)
                .WithEndTime(context.EndTime);

            var command = new PlanMaintenanceJobCommandBuilder()
                .WithMaintenanceJobBuilder(context.MaintenanceJobBuilder)
                .Build();

            context.WorkshopPlanning.PlanMaintenanceJob(command);
        }

        [Then(@"the WorkshopPlanning should contain (.*) jobs")]
        public void ThenTheWorkshopPlanningShouldContainJobs(int numberJobs)
        {
            Assert.Equal(numberJobs, context.WorkshopPlanning.Jobs.Count);
        }

        [Given(@"I have a existing workshop planning")]
        public void GivenIHaveAExistingWorkshopPlanningFor()
        {
            if (context.WorkshopPlanning == null)
                throw new InvalidOperationException("No Planning found");
        }

        [When(@"I view the planning for that date")]
        public void WhenIViewThePlanningForThatDate()
        {
            //TODO
        }

        [Then(@"I see (.*) maintenance job in the workshop planning")]
        public void ThenISeeMaintenanceJobInTheWorkshopPlanning(int numberJobs)
        {
            Assert.Equal(numberJobs, context.WorkshopPlanning.Jobs.Count);
        }

        [When(@"I select a maintenance job")]
        public void WhenISelectAMaintenanceJob()
        {
            context.SelectedJob = context.WorkshopPlanning.Jobs.First();
        }

        [Then(@"I see the job status is (.*)")]
        public void ThenISeeTheJobStatusIsPlanned(string status)
        {
            Assert.Equal(status, context.SelectedJob.Status);
        }

        [When(@"complete the selected job")]
        public void WhenCompleteTheSelectedJob()
        {
            //TODO
        }
    }
}
