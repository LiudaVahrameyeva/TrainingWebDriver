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
    }
}