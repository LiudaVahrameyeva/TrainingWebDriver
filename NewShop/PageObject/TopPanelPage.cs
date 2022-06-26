using System;
using System.Threading;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class TopPanelPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();

        private IWebElement WomenButton => driver.FindElement(By.XPath("//*[text()='Women']"));

        private IWebElement ViewCart => driver.FindElement(By.CssSelector("[title='View my shopping cart']"));
        private IWebElement WomenText => driver.FindElement(By.CssSelector("span.cat-name"));

        public void WomenButtonClick()
        {
            WomenButton.Click();
        }

        public void AccountClick()
        {
            var getSettings =  SettingsReader.ReadSettings();
            IWebElement AccountButton = driver.FindElement(By.CssSelector("a.account"));
            if (getSettings.Browser.Contains("firefox"))
            {
                ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].scrollIntoView(true);", AccountButton);
             
            }
            Thread.Sleep(1000);
            AccountButton.Click();
        }

        public void ViewCartClick()
        {
            ViewCart.Click();
        }

        public void ClickWomenText()
        {
            WomenText.Click();
        }
        
        public void WaitUntilWomenLinkIsVisible(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//*[text()='Women']")));
        }
    }

}