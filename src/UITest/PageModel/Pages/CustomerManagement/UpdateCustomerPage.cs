using OpenQA.Selenium;

namespace Pitstop.UITest.PageModel.Pages.CustomerManagement
{
    public class UpdateCustomerPage : PitstopPage
    {
        public UpdateCustomerPage(PitstopApp pitstop) : base("Customer Management - edit customer", pitstop)
        {
        }

        public UpdateCustomerPage FillCustomerDetails(string name, string address,
            string city, string postalCode, string telephoneNumber, string emailAddress)
        {

            var nameElement = WebDriver.FindElement(By.Name("Customer.Name"));
            nameElement.Clear();
            nameElement.SendKeys(name);

            var addressElement = WebDriver.FindElement(By.Name("Customer.Address"));
            addressElement.Clear();
            addressElement.SendKeys(address);

            var postalCodeElement = WebDriver.FindElement(By.Name("Customer.PostalCode"));
            postalCodeElement.Clear();
            postalCodeElement.SendKeys(postalCode);

            var cityElement = WebDriver.FindElement(By.Name("Customer.City"));
            cityElement.Clear();
            cityElement.SendKeys(city);

            var telephoneNumberElement = WebDriver.FindElement(By.Name("Customer.TelephoneNumber"));
            telephoneNumberElement.Clear();
            telephoneNumberElement.SendKeys(telephoneNumber);

            var emailAddressElement = WebDriver.FindElement(By.Name("Customer.EmailAddress"));
            emailAddressElement.Clear();
            emailAddressElement.SendKeys(emailAddress);

            return this;
        }

        public CustomerManagementPage Submit()
        {
            WebDriver.FindElement(By.Id("SubmitButton")).Click();
            return new CustomerManagementPage(Pitstop);
        }

        public CustomerManagementPage Cancel()
        {
            WebDriver.FindElement(By.Id("CancelButton")).Click();
            return new CustomerManagementPage(Pitstop);
        }
    }
}
