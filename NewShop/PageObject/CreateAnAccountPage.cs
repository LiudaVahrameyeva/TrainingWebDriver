using System;
using NewShop.BaseSetup;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;


namespace NewShop.PageObject
{

    public class CreateAnAccountPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();

        private UserInfo userInfo = SettingsReader.ReadInfo();
        private IWebElement FirstNameCustomer => driver.FindElement(By.CssSelector("[id='customer_firstname']"));
        private IWebElement LastNameCustomer => driver.FindElement(By.CssSelector("[id='customer_lastname']"));
        private IWebElement Email => driver.FindElement(By.CssSelector("[id='email']"));
        private IWebElement Password => driver.FindElement(By.CssSelector("[id='passwd']"));
        private IWebElement FirstName => driver.FindElement(By.CssSelector("[id='firstname']"));
        private IWebElement LastName => driver.FindElement(By.CssSelector("[id='lastname']"));
        private IWebElement Address => driver.FindElement(By.CssSelector("[id='address1']"));
        private IWebElement City => driver.FindElement(By.CssSelector("[id='city']"));
        private IWebElement State => driver.FindElement(By.CssSelector("[id='id_state']"));
        private IWebElement Zip => driver.FindElement(By.CssSelector("[id='postcode']"));
        private IWebElement Country => driver.FindElement(By.CssSelector("[id='id_country']"));
        private IWebElement PhoneMobile => driver.FindElement(By.CssSelector("[id='phone_mobile']"));

        private IWebElement AddressAlias => driver.FindElement(By.CssSelector("[id='alias']"));
        private IWebElement SubmitAccount => driver.FindElement(By.CssSelector("[id='submitAccount']"));

        public void EnterFirstNameCustomer(string firstName)
        {
            WaitUntilPageIsVisible();
            FirstNameCustomer.SendKeys(firstName);
        }

        public void EnterLastNameCustomer(string lastName)
        {
            LastNameCustomer.SendKeys(lastName);
        }

        public void EnterEmailAddress(string email)
        {
            Email.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void EnterFirstName(string firstName)
        {
            FirstName.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            LastName.SendKeys(lastName);
        }

        public void EnterAddress(string address)
        {
            Address.SendKeys(address);
        }

        public void SelectState(string state)
        {
            var selectElement = new SelectElement(State);

            selectElement.SelectByText(state);
        }

        public void EnterCity(string city)
        {
            City.SendKeys(city);
        }

        public void EnterZip(string zip)
        {
            Zip.SendKeys(zip);
        }

        public void SelectCountry(string country)
        {
            var selectElement = new SelectElement(Country);

            selectElement.SelectByText(country);
        }

        public void EnterPhoneMobile(string phoneMobile)
        {
            PhoneMobile.SendKeys(phoneMobile);
        }

        public void EnterAddressAlias(string addressAlias)
        {
            MakeClearAddressAlias();
            AddressAlias.SendKeys(addressAlias);
        }

        public void MakeClearAddressAlias()
        {
            AddressAlias.Clear();
        }

        public void ClickRegister()
        {
            SubmitAccount.Click();
        }

        public void CreateAnAccount()
        {
            EnterFirstNameCustomer(userInfo.FirstName);
            EnterLastNameCustomer(userInfo.LastName);
            EnterPassword(userInfo.Password);
            EnterFirstName(userInfo.FirstName);
            EnterLastName(userInfo.LastName);
            EnterAddress(userInfo.Address);
            EnterCity(userInfo.City);
            SelectState(userInfo.State);
            EnterZip(userInfo.Zip);
            SelectCountry(userInfo.Country);
            EnterPhoneMobile(userInfo.PhoneMobile);
            EnterAddressAlias(userInfo.AddressAlias);
            ClickRegister();
        }

        public void WaitUntilPageIsVisible(int secondsToWait = 1000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//div[@id = 'uniform-id_gender1']")));
        }
    }
}