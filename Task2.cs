//Create test 1, which goes to https://www.yandex.by/,
//login with correct credentials (create a new account;
//create new project for that in Visual Studio; choose testing framework NUnit3*).
//Do not forget to add assertion in your test.


using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject2
{

    public class Tests
    {
        private WebDriver driver;

        private LoginPage _loginPage => new LoginPage(driver);

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

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
            Assert.IsTrue(_loginPage.Login.Enabled);
            
        }
        
        [Test]
        public void EnterLoginPass()
        {
            GoToYandexMail();
            _loginPage.SetLogin("LiudaVahrameyeva");
            if (!_loginPage.PasswordIsDisplayed())
            {
                _loginPage.ClickEnter();  
            }

            _loginPage.WaitUntilPasswordIsVisible(10);
            _loginPage.SetPassword("Password#123");
            _loginPage.ClickEnter();
            _loginPage.WaitUntilAvatarIsVisible(5);
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

