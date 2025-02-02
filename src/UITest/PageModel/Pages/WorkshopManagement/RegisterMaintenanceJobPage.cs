using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pitstop.UITest.PageModel.Pages.WorkshopManagement
{
    /// <summary>
    /// Represents the Register MaintenanceJob page.
    /// </summary>
    public class RegisterMaintenanceJobPage : PitstopPage
    {
        public RegisterMaintenanceJobPage(PitstopApp pitstop) : base("Workshop Management - schedule maintenance", pitstop)
        {
        }

        public RegisterMaintenanceJobPage FillJobDetails(string startTime, string endTime, string description, string licenseNumber)
        {
            var startTimeBox = WebDriver.FindElement(By.Name("StartTime"));
            startTimeBox.Clear();
            startTimeBox.SendKeys(startTime);

            var endTimeBox = WebDriver.FindElement(By.Name("EndTime"));
            endTimeBox.Clear();
            endTimeBox.SendKeys(endTime);

            WebDriver.FindElement(By.Name("Description")).SendKeys(description);
            var webElement = WebDriver.FindElement(By.Id("SelectedVehicleLicenseNumber"));
            SelectElement select = new SelectElement(webElement);
            select.SelectByValue(licenseNumber);
            
            return this;
        }

        public WorkshopManagementPage Submit()
        {
            WebDriver.FindElement(By.Id("SubmitButton")).Click();
            return new WorkshopManagementPage(Pitstop);
        }

        public WorkshopManagementPage Cancel()
        {
            WebDriver.FindElement(By.Id("CancelButton")).Click();
            return new WorkshopManagementPage(Pitstop);
        }
    }
}