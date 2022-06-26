using NewShop.BaseSetup;
using OpenQA.Selenium;

namespace NewShop.PageObject
{

    public class MyAccountPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();

        private IWebElement MyAccount => driver.FindElement(By.XPath("//span[text()='My account']"));
        private IWebElement MyWishlistsButton => driver.FindElement(By.XPath("//*[text()='My wishlists']"));

        public bool MyAccountIsDisplayed()
        {
            return MyAccount.Enabled;
        }

        public void ClickMyWishLists()
        {
            MyWishlistsButton.Click();
        }

    }
}