using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NewShop.BaseSetup;
using NewShop.PageObject;

namespace NewShop.Test
{
    [AllureNUnit]
    [AllureSuite("Tests for Task AP-2")]
    [TestFixture]
    public class AP2Task
    {
        private HomePage homePage = new HomePage();
        private LoginPage loginPage = new LoginPage();
        private MyAccountPage myAccountPage = new MyAccountPage();
      

        [SetUp]
        public void Setup()
        {
            homePage.Navigate("http://automationpractice.com/index.php?controller=authentication&back=my-account");
        }

        [Test]
        [AllureTms("Verify the ability to login in account")]
        public void AP2Test()
        {
            //Fill Email address and Password inputs and Click Sign In
            loginPage.Login();
            Assert.IsTrue(myAccountPage.MyAccountIsDisplayed(),"My account page is not displayed");
        }
        
        [TearDown]
        public void Cleanup()
        {
            SetupWebDriver.StopDriver();
        }
    }
}