using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    public class Page
    {
        protected static WebDriver Driver { get; set; }
        
        protected readonly By Locator;
        
        private IWebElement Find()
        {
            return Driver.FindElement(Locator);
        }

        public bool IsDisplayed()
        {

            bool isDisplayed;

            try
            {
                isDisplayed = Find().Displayed;
            }

            catch (NoSuchElementException)
            {
                isDisplayed = false;
            }
            
            return isDisplayed;
        }
        
    }
}