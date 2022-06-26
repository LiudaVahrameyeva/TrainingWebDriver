using System;
using System.Collections.Generic;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class ProdDialogPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();
        private IWebElement ProdName => driver.FindElement(By.CssSelector("[id='layer_cart_product_title']"));
        private IWebElement ContinueShopping => driver.FindElement(By.CssSelector("[title='Continue shopping']"));
        private IWebElement Close => driver.FindElement(By.CssSelector("[title='Close window']"));

        public string GetProductName()
        {
            return ProdName.Text;
        }

        public void ClickContinueShopping()
        {
            WaitUntilContinueButtonIsVisible();
            ContinueShopping.Click();
        }

        public void ClickClose()
        {
            WaitUntilContinueButtonIsVisible();
            Close.Click();
        }


        public void WaitUntilContinueButtonIsVisible(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.CssSelector("[title='Close window']")));
        }
    }
}