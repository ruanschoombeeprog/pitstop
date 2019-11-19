using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pitstop.UITest.PageModel.Pages.VehicleManagement
{
    public class UpdateVehiclePage : PitstopPage
    {
        public UpdateVehiclePage(PitstopApp pitstop) : base("Vehicle Management - update vehicle", pitstop)
        {
        }

        public UpdateVehiclePage FillVehicleDetails(string licenseNumber, string brand, string type, string owner)
        {
            var brandField = WebDriver.FindElement(By.Name("Vehicle.Brand"));
            brandField.Clear();
            brandField.SendKeys(brand);

            var typeField = WebDriver.FindElement(By.Name("Vehicle.Type"));
            typeField.Clear();
            typeField.SendKeys(type);

            var select = new SelectElement(WebDriver.FindElement(By.Id("SelectedCustomerId")));
            select.SelectByText(owner);

            return this;
        }

        public VehicleManagementPage Submit()
        {
            WebDriver.FindElement(By.Id("SubmitButton")).Click();
            return new VehicleManagementPage(Pitstop);
        }

        public VehicleManagementPage Cancel()
        {
            WebDriver.FindElement(By.Id("CancelButton")).Click();
            return new VehicleManagementPage(Pitstop);
        }
    }
}
