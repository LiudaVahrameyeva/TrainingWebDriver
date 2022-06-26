using System;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class HomePage
    {

        private IWebDriver driver = SetupWebDriver.GetDriver();
        private IWebElement SignIn => driver.FindElement(By.CssSelector("a.login"));

        public void Navigate(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

    }
}