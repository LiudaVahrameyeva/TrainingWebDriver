using System.Linq;
using System.Reflection;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NewShop.BaseSetup;
using NewShop.PageObject;

namespace NewShop.Test
{

        [AllureNUnit]
        [AllureSuite("Tests for Task AP-5")]
        public class AP5Task
        {
            private HomePage homePage = new HomePage();
            private LoginPage loginPage = new LoginPage();
            private ClothesPage clothesPage = new ClothesPage();
            private TopPanelPage topPanel = new TopPanelPage();
            private ShoppingCartPage shoppingCartPage = new ShoppingCartPage();

            [SetUp]
            public void Setup()
            {
                homePage.Navigate("http://automationpractice.com/index.php?controller=authentication&back=my-account");
            }

            [Test]
            [AllureTms("Verify the ability to add to cart")]
            public void AP5Test()
            {
                //Login
                loginPage.Login();
                //Add 3 different products to cart
                topPanel.WomenButtonClick();
                var expectedList = clothesPage.AddItemsToCard(3);
                topPanel.ViewCartClick();
                var actualList = shoppingCartPage.GetList();
                Assert.AreEqual(3,actualList.Count, "The quantity are not the same.");
                bool isEqual = Enumerable.SequenceEqual(expectedList.OrderBy(e => e), actualList.OrderBy(e => e));
                Assert.IsTrue(isEqual ,"The list of products are not the same.");

            }

            [TearDown]
            public void Cleanup()
            {
                SetupWebDriver.StopDriver();
            }
        }
    
}