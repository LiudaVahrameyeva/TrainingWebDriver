using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NewShop.BaseSetup;
using NewShop.PageObject;

namespace NewShop.Test
{
    [AllureNUnit]
    [AllureSuite("Tests for Task AP-3")]
    [TestFixture]
    public class AP3Task
    {
        private HomePage homePage = new HomePage();
        private LoginPage loginPage = new LoginPage();
        private MyAccountPage myAccountPage = new MyAccountPage();
        private MyWishlistsPage myWishlistsPage = new MyWishlistsPage();
        private ClothesPage clothesPage = new ClothesPage();
        private TopPanelPage topPanel = new TopPanelPage();
        const string defaultWishlistName = "My wishlist";
        
        [SetUp]
        public void Setup()
        {
            homePage.Navigate("http://automationpractice.com/index.php?controller=authentication&back=my-account");
        }

        [Test]
        [AllureTms("Verify the ability to add to auto-created Wishlist")]
        public void AP3Test()
        {
            //Login
            loginPage.Login();
            //Make sure that there is no Wishlist in account settings
            myAccountPage.ClickMyWishLists();
            myWishlistsPage.CleanWishlist();
            //Go to any product detail page and click Add to Wishlist button
            topPanel.WomenButtonClick();
            clothesPage.AddItemsToWishlist(1);
            //Go to My Account-> Wishlist
            topPanel.AccountClick();
            myAccountPage.ClickMyWishLists();
            var list = myWishlistsPage.GetList();
            Assert.AreEqual(1,list.Count,"My Wishlist wasn't added");
            Assert.AreEqual(defaultWishlistName, list[0].Name,"My Wishlist wasn't added");
        }
        
        [TearDown]
        public void Cleanup()
        {
            SetupWebDriver.StopDriver();
        }
    }
}