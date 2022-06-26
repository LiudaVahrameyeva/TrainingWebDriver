using System;
using NewShop.BaseSetup;
using NewShop.PageObject;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;


namespace NewShop.Test
{
    [AllureNUnit]
    [AllureSuite("Tests for Task AP-1")]
    [TestFixture]
    public class AP1Task
    
    {
        private HomePage homePage = new HomePage();
        private LoginPage loginPage = new LoginPage();
        private CreateAnAccountPage createAnAccountPage = new CreateAnAccountPage();
        private MyAccountPage myAccountPage = new MyAccountPage();

        [SetUp]
        public void Setup()
        {
            homePage.Navigate("http://automationpractice.com/index.php?controller=authentication&back=my-account");
        }

        [Test]
        [AllureTms("Verify the ability to create an account")]
        public void AP1Test()
        {
            //Fill email address input and click Create an account button
      
            loginPage.VerifyEmail(GetRandomEmail());
            loginPage.ClickCreateAnAccount();
            Assert.IsTrue(loginPage.CreateAccountErrorExists(), "An account using this email address has already been registered.");
            
            //Fill all required fields and click Register Button
            createAnAccountPage.CreateAnAccount();
            Assert.IsTrue(myAccountPage.MyAccountIsDisplayed(),"My account page is not displayed");
        }



        private string GetRandomEmail()
        {
            var date = DateTime.Now.ToString("HHmmss");
            return $"VasyaPupkin{date}@tut.by";
        }
        

        [TearDown]
        public void Cleanup()
        {
            SetupWebDriver.StopDriver();
        }
    }
}