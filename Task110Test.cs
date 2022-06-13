using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace TestProject2
{
    public class Task110Test
    {
        private WebDriver driver;

        [SetUp]
        public void Setup()
        {
            var chrome = new ChromeOptions();
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), chrome);
        }

        [Test]
        public void TestForSelenoid()
        {
            HomePage homePage = new HomePage(driver);

            homePage.Navigate();
            homePage.SetSearchValue("Cool training!");

            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile("c:\\tmp\\screenshot.png", ScreenshotImageFormat.Png);

        }
        
        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}