using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pitstop.UITest.PageModel.Pages.VehicleManagement
{
    /// <summary>
    /// Represents the VehicleDetails page.
    /// </summary>
    public class VehicleDetailsPage : PitstopPage
    {        
        public VehicleDetailsPage(PitstopApp pitstop) : base("Vehicle Management - details", pitstop)
        {
        }

        public UpdateVehiclePage Edit()
        {
            WebDriver.FindElement(By.LinkText("Edit")).Click();
            return new UpdateVehiclePage(Pitstop);
        }

        public VehicleManagementPage Back()
        {
            WebDriver.FindElement(By.Id("BackButton")).Click();
            return new VehicleManagementPage(Pitstop);
        }
    }
}