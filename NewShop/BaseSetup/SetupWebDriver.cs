using System;
using System.Collections.Generic;
using Allure.Commons;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace NewShop.BaseSetup
{
    public class SetupWebDriver
    {
        private static WebDriver driver;
        private static WebDriverWait driverWait;

        public static WebDriver GetDriver()
        {
            if (driver == null)
            {
                StartDriver();
            }
            return driver;
        }

        private static void SetDriver(WebDriver value)
        {
            driver = value;
        }

        private static WebDriverWait GetDriverWait()
        {
            if (driverWait == null || driver == null)
            {
                throw new NullReferenceException(
                    "The WebDriver wait instance was not initialized. You should first call the method Start.");
            }

            return driverWait;
        }
        
        private static void SetDriverWait(WebDriverWait value)
        {
            driverWait = value;
        }

        private static void StartDriver()
        {
        //private Settings mySettings = SettingsReader.ReadSettings();
            var getSettings =  SettingsReader.ReadSettings();

            switch (@getSettings.Strategy.Trim().ToLower())
            {
                case ("local"):
                    SetLocalDriver(getSettings.Browser.Trim().ToLower());
                    break;
                case ("selenoid"):
                    SetRemoteDriver(getSettings.Strategy.Trim().ToLower(), getSettings.Browser.Trim().ToLower());
                    break;
                case ("saucelabs"):
                    SetRemoteDriver(getSettings.Strategy.Trim().ToLower(), getSettings.Browser.Trim().ToLower());
                    break;
                default:
                    throw new ArgumentException("You need to set a valid settings.");
            }
            
            SetDriverWait(new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(30)));
        }

        private static void SetLocalDriver(string getBrowser)
        {
            switch (@getBrowser)
            {
                case ("chrome"):
                    SetDriver(new ChromeDriver());
                    break;
                case ("firefox"):
                    SetDriver(new FirefoxDriver());
                    break;
                default:
                    throw new ArgumentException("You need to set a valid browser type.");
            }
            driver.Manage().Window.Maximize();
        }

        private static void SetRemoteDriver(string strategy, string getBrowser)
        {
            DriverOptions options = null;
            Uri sauceUrl = null;
            
            switch (@getBrowser)
            {
                case ("chrome"):
                    options = new ChromeOptions();
                    break;
                case ("firefox"):
                    options = new FirefoxOptions();
                    break;
                default:
                    throw new ArgumentException("You need to set a valid browser type.");
            }
            
            switch (strategy)
            {
                case ("selenoid"):
                    sauceUrl = new Uri("http://localhost:4444/wd/hub");
                    SetDriver(new RemoteWebDriver(sauceUrl, options));
                    break;
                case ("saucelabs"):
                    String currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    String currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
                    var browserOptions = options;
                    browserOptions.PlatformName = "Windows 10";
                    browserOptions.BrowserVersion = null;

                    var sauceOptions = new Dictionary<string, object>();
                    sauceOptions.Add("build", "Platform Configurator Build " + currentDate);
                    sauceOptions.Add("job", "Platform Configurator Job " + currentTime);
                    sauceOptions.Add("username", Environment.GetEnvironmentVariable("SAUCE_USERNAME"));
                    sauceOptions.Add("accessKey", Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY"));
                    browserOptions.AddAdditionalOption("sauce:options", sauceOptions);
                    
                    sauceUrl = new Uri("https://ondemand.eu-central-1.saucelabs.com:443/wd/hub");
                    SetDriver(new RemoteWebDriver(sauceUrl, browserOptions));

                    ((IJavaScriptExecutor) GetDriver()).ExecuteScript($"sauce:job-name= {TestContext.CurrentContext.Test.Name}");
                  
                    break;
                default:
                    throw new ArgumentException("You need to select a valid strategy.");
            }
        }

        public static void StopDriver()
        {
            var settings = SettingsReader.ReadSettings();
            if (!settings.Strategy.Equals("saucelabs"))
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                if (status == TestStatus.Failed)
                {
                    AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png",
                        ((ITakesScreenshot) GetDriver()).GetScreenshot().AsByteArray);
                }
            }
            else
            {
                SauceLabsClean();
            }

            Console.WriteLine($"Date and Time: {DateTime.Now}");
            Console.WriteLine($"Browser Name: {GetDriver().Capabilities.GetCapability("browserName")}");
            Console.WriteLine($"Browser Version: {GetDriver().Capabilities.GetCapability("browserVersion")}");
            Console.WriteLine($"Platform Version: {GetDriver().Capabilities.GetCapability("platformName")}");
            
            GetDriver().Quit();
            SetDriver(null);
            SetDriverWait(null);
            
        }
       
        
        public static void CleanCookies()
        {
            GetDriver().Manage().Cookies.DeleteAllCookies();
        }

        public static void SauceLabsClean()
        {
            var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            var script = "sauce:job-result=" + (isPassed ? "passed" : "failed");
            ((IJavaScriptExecutor) driver).ExecuteScript(script);
        }

    }
}