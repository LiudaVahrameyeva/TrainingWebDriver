using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NewShop.BaseSetup;
using NewShop.PageObject;

namespace NewShop.Test
{
    [AllureNUnit]
    [AllureSuite("Tests for Task AP-4")]
    [TestFixture]
    public class AP4Task
    {
        private HomePage homePage = new HomePage();
        private LoginPage loginPage = new LoginPage();
        private MyAccountPage myAccountPage = new MyAccountPage();
        private MyWishlistsPage myWishlistsPage = new MyWishlistsPage();
        private ClothesPage clothesPage = new ClothesPage();
        private TopPanelPage topPanel = new TopPanelPage();
        const string newWishlist = "Favorite";
        [SetUp]
        public void Setup()
        {
            homePage.Navigate("http://automationpractice.com/index.php?controller=authentication&back=my-account");
        }

        [Test]
        [AllureTms("Verify the ability to add to your Wishlist")]
        public void AP4Test()
        {
            //Login
            loginPage.Login();
            //Make sure that there is no Wishlist in account settings
            myAccountPage.ClickMyWishLists();
            myWishlistsPage.CleanWishlist();
            //Create Wishlist in account settings
            myWishlistsPage.AddNewWishlist(newWishlist);
            var list = myWishlistsPage.GetList();
            Assert.AreEqual(newWishlist, list[0].Name,"My Wishlist wasn't added");
            //Go to any product detail page and click Add to Wishlist button
            topPanel.WomenButtonClick();
            var product = clothesPage.AddItemsToWishlist(1);
            //Go to My Account-> Wishlist
            topPanel.AccountClick();
            myAccountPage.ClickMyWishLists();
            //Open the Wishlist that we have created and verify product
            myWishlistsPage.OpenWishlist(newWishlist);
            var actualProd = myWishlistsPage.GetProductName();
            Assert.AreEqual(product[0].Name,actualProd,
                $"Correct product wasn't added. Expected: {product[0].Name}, got: {actualProd}.");
     
        }
        [TearDown]
        public void Cleanup()
        {
            SetupWebDriver.StopDriver();
        }
    }
}