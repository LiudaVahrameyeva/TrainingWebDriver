using System;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{
    [AllureNUnit]
    [AllureSuite("Tests for Task 80")]
    public class Task80Test
    {
        private WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        [AllureTms("Screenshot")]
        public void TakeScreenshot()
        {
            HomePage homePage = new HomePage(driver);

            homePage.Navigate();

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