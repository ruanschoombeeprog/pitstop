using System;
using Xunit;
using Xunit.Abstractions;
using Pitstop.UITest.PageModel;

namespace Pitstop.UITest
{
    public class ScenarioTests
    {
        private readonly ITestOutputHelper _output;

        public ScenarioTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void End_To_End()
        {
            // arrange
            string testrunId = Guid.NewGuid().ToString("N");
            PitstopApp pitstop = new PitstopApp(testrunId, TestConstants.PitstopStartUrl);
            var homePage = pitstop.Start();

            // act

            // Register Customer
            pitstop.Menu
                .CustomerManagement()
                .RegisterCustomer()
                .Cancel()
                .RegisterCustomer()
                .FillCustomerDetails(
                    $"TestCustomer {testrunId}", "Verzonnenstraat 21",
                    "Uitdeduimerveen", "1234 AZ", "+31612345678", "tc@test.com")
                .Submit()
                .SelectCustomer($"TestCustomer {testrunId}")
                .Back();

            // Update Customer
            pitstop.Menu
               .CustomerManagement()
               .SelectCustomer($"TestCustomer {testrunId}")
               .Edit()
               .FillCustomerDetails(
                    $"TestCustomer {testrunId}", "Verzonnenstraat 21",
                    "Uitdeduimerveen", "1234 AZ", "+31612345678", "tc@test.com")
               .Submit()
               .SelectCustomer($"TestCustomer {testrunId}")
               .Edit()
               .Cancel();

            // Register vehicle
            pitstop.Menu
                .VehicleManagement()
                .RegisterVehicle()
                .Cancel()
                .RegisterVehicle()
                .FillVehicleDetails($"Vehicle {testrunId}", "Testla", "Model T", $"TestCustomer {testrunId}")
                .Submit()
                .SelectVehicle($"Vehicle {testrunId}")
                .Back();

            // Update vehicle
            pitstop.Menu
               .VehicleManagement()
               .SelectVehicle($"Vehicle {testrunId}")
               .Edit()
               .FillVehicleDetails($"Vehicle {testrunId}", "Testla 123", "Model T", $"TestCustomer {testrunId}")
               .Submit()
               .SelectVehicle($"Vehicle {testrunId}")
               .Edit()
               .Cancel();

            // Register Maintenance Job
            pitstop.Menu
                .WorkshopManagement()
                .RegisterMaintenanceJob()
                .Cancel()
                .RegisterMaintenanceJob()
                .FillJobDetails("08:00", "12:00", $"Job {testrunId}", $"Vehicle {testrunId}")
                .Submit()
                .SelectMaintenanceJob($"Job {testrunId}")
                .Back();

            // Update Maintenance Job
            pitstop.Menu
                .WorkshopManagement()
                .SelectMaintenanceJob($"Job {testrunId}")
                .Edit()
                .FillJobDetails("08:00", "12:00", $"Job {testrunId}", $"Vehicle {testrunId}")
                .Submit()
                .SelectMaintenanceJob($"Job {testrunId}")
                .Edit()
                .Cancel();

            // Finish Maintenance Job
            pitstop.Menu
                .WorkshopManagement()
                .SelectMaintenanceJob($"Job {testrunId}")
                .GetJobStatus(out string beforeJobStatus)
                .Complete()
                .FillJobDetails("08:00", "11:00", $"Mechanic notes {testrunId}")
                .Complete()
                .GetJobStatus(out string afterJobStatus)
                .Back();

            // assert
            Assert.Equal("Planned", beforeJobStatus);
            Assert.Equal("Completed", afterJobStatus);

            // cleanup
            pitstop.Stop();
        }
    }
}
