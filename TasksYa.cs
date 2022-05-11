//Create test 1, which goes to https://www.yandex.by/,
//login with correct credentials (create a new account;
//create new project for that in Visual Studio; choose testing framework NUnit3*).
//Do not forget to add assertion in your test.


using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{

    public class TasksYa
    {
        private WebDriver driver;

        private LoginPage _loginPage => new LoginPage(driver);

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        [Test]
        public void LaunchYandex()
        {
            driver.Navigate().GoToUrl("https://www.yandex.by/");
 
            Assert.IsTrue(_loginPage.FindButtonIsDisplayed());
        }

        [Test]
        public void LoginEnabled()
        {
            GoToYandexMail();
            Thread.Sleep(2000); //Explicit Wait Type. suspend or pause the current thread execution for a specified time
            Assert.IsTrue(_loginPage.Login.Enabled);
            
        }

        [Test, TestCaseSource(typeof(TestData),nameof(TestData.TypeLogin))]
        public void EnterLoginPass(string userLogin)
        {

                GoToYandexMail();
                _loginPage.SetLogin(userLogin);
                if (!_loginPage.PasswordIsDisplayed())
                {
                    _loginPage.ClickEnter();
                }
                _loginPage.WaitUntilPasswordIsVisible(5);
                _loginPage.SetPassword("Password#123");
                _loginPage.ClickEnter();
                _loginPage.WaitUntilNameIsVisible(5);
                Assert.IsTrue(_loginPage.Avatar.Enabled);
         
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }

        private void GoToYandexMail()
        {
            driver.Navigate().GoToUrl("https://www.yandex.by/");
            _loginPage.MailEnterClick();
        }
    }
}

