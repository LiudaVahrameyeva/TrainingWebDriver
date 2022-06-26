using System;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class ProductDetailPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();
        private IWebElement AddToWishlist => driver.FindElement(By.CssSelector("[id='wishlist_button']"));
        private IWebElement ProductName => driver.FindElement(By.CssSelector("[itemprop='name']"));
        private IWebElement Model => driver.FindElement(By.CssSelector("[itemprop='sku']"));
        private IWebElement CloseButton => driver.FindElement(By.CssSelector("[title='Close']"));

        private IWebElement AddToCardButton => driver.FindElement(By.CssSelector("[name='Submit']"));

        //private IWebElement ProductInfoDialog => driver.FindElement(By.CssSelector("layer_cart"));
        private IWebElement CloseProductInfoDialogButton => driver.FindElement(By.CssSelector("span.cross"));

        public void ClickAddToWishlist()
        {
            AddToWishlist.Click();
        }

        public string GetProductName()
        {
            return ProductName.Text;
        }

        public string GetModel()
        {
            return Model.Text;
        }

        public void ClickCloseButton()
        {
            WaitUntilMessageIsVisible();

            CloseButton.Click();

        }
        
        public void ClickClose()
        {

            CloseButton.Click();

        }

        public void ClickAddToCard()
        {
            AddToCardButton.Click();
        }


        public void WaitUntilMessageIsVisible(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//a[@class = 'fancybox-item fancybox-close']")));
        }
        public void WaitUntilAddToWishlistIsVisible(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("[id='wishlist_button']")));
        }

    }
}