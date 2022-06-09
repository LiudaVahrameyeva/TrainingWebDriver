using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    public class Task120Test
    {

        private RemoteWebDriver driver;
        private Task50Methods _task50Methods => new Task50Methods(driver);
        
        [SetUp]
        public void SetupTest()
        {
            GetSettings settings = new GetSettings();
            var item = settings.GetItem();
            String currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            String currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            var browserOptions = SelectBrowserAndPlatform(item.browser);
            browserOptions.PlatformName = item.platformName;
            browserOptions.BrowserVersion = item.browserVersion;

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("build", "Platform Configurator Build " + currentDate);
            sauceOptions.Add("job", "Platform Configurator Job " + currentTime);
            sauceOptions.Add("username", Environment.GetEnvironmentVariable("SAUCE_USERNAME"));
            sauceOptions.Add("accessKey", Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY"));
            browserOptions.AddAdditionalOption("sauce:options", sauceOptions);
            
           var sauceUrl = new Uri("https://ondemand.eu-central-1.saucelabs.com:443/wd/hub");

            driver = new RemoteWebDriver(sauceUrl, browserOptions);
            ((IJavaScriptExecutor) driver).ExecuteScript($"sauce:job-name= {TestContext.CurrentContext.Test.MethodName}");
        }

        [Test]
        public void WaitForUser()
        {
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/dynamic-data-loading-demo.html");

            _task50Methods.GetNewUserClick();

            Assert.True(_task50Methods.FirstAndLastNameIsDisplayed(), "The First and Last name are not displayed yet.");

        }

   

        private DriverOptions SelectBrowserAndPlatform(string browser)
        {
            DriverOptions option = null;
      
            switch (browser)
            {
      
                case "chrome":
                    option = new ChromeOptions();
                     break;
                case "edge":
                    option = new EdgeOptions();
                    break;
                case "firefox":
                    option = new FirefoxOptions();
                    break;
                default:
                    Console.WriteLine("default settings");
                    break;
            }

            return option;
        }

        [TearDown]
        public void Cleanup()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            var script = "sauce:job-result=" + (isPassed ? "passed" : "failed");
            ((IJavaScriptExecutor) driver).ExecuteScript(script);

            driver?.Quit();
        }
        
    }
}