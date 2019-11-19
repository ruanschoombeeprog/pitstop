using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pitstop.UITest.PageModel.Pages.WorkshopManagement
{
    public class UpdateMaintenanceJobPage : PitstopPage
    {
        public UpdateMaintenanceJobPage(PitstopApp pitstop) : base("Workshop Management - update maintenance", pitstop)
        {
        }

        public UpdateMaintenanceJobPage FillJobDetails(string startTime, string endTime, string description, string licenseNumber)
        {
            var startTimeBox = WebDriver.FindElement(By.Name("StartTime"));
            startTimeBox.Clear();
            startTimeBox.SendKeys(startTime);

            var endTimeBox = WebDriver.FindElement(By.Name("EndTime"));
            endTimeBox.Clear();
            endTimeBox.SendKeys(endTime);

            var descriptionElement = WebDriver.FindElement(By.Name("Description"));
            descriptionElement.Clear();
            descriptionElement.SendKeys(description);

            var selectElement = WebDriver.FindElement(By.Id("SelectedVehicleLicenseNumber"));
            var select = new SelectElement(selectElement);
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
