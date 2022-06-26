using System;
using System.Collections.Generic;
using System.Threading;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V101.Input;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class MyWishlistsPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();
        private IWebElement DeleteButton => driver.FindElement(By.CssSelector("i.icon-remove"));
        private IWebElement TableWishlist => driver.FindElement(By.XPath("//table[@class='table table-bordered']"));
        private IWebElement ProductName => driver.FindElement(By.CssSelector("p.product-name"));
        private IWebElement Name => driver.FindElement(By.XPath("//input[@id='name']"));
        private IWebElement SaveButton => driver.FindElement(By.CssSelector("[id='submitWishlist']"));

        private void CleanNameForWishlist()
        {
            Name.Clear();
        }

        public void EnterNameForWishlist(string name)
        {
            CleanNameForWishlist();
            Name.SendKeys(name);
        }

        public void ClickSave()
        {
            SaveButton.Click();
        }

        public void AddNewWishlist(string name)
        {
            EnterNameForWishlist(name);
            ClickSave();
            WaitUntilTableIsVisible();
        }

        private string GetElement(int rowNum, int colNum)
        {
            return driver.FindElement(By.XPath(
                $"//table[@class='table table-bordered']//tbody//tr[{rowNum}]/td[{colNum}]")).Text;
        }

        private IWebElement GetIWebElement(int rowNum, int colNum)
        {
            return driver.FindElement(By.XPath(
                $"//table[@class='table table-bordered']//tbody//tr[{rowNum}]/td[{colNum}]"));
        }

        private bool TableWishListExists()
        {
            var getSettings =  SettingsReader.ReadSettings();
            try
            {
                if (getSettings.Browser.Contains("firefox"))
                {
                    IWebElement TableWishlist = driver.FindElement(By.XPath("//table[@class='table table-bordered']"));
                    ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].scrollIntoView(true);", TableWishlist);
             
                }
                return TableWishlist.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void CleanWishlist()
        {
            if (TableWishListExists())
            {
                int count = GetList().Count;
                for (int i = 0; i < count; i++)
                {
                    DeleteButton.Click();
                    CloseConfirm();
                }
            }
        }

        /*private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }*/

        public IList<Additional.Wishlists> GetList()
        {

            List<Additional.Wishlists> listOfWishlists = new List<Additional.Wishlists>();
            IList<IWebElement> rows = null;

            var headersDict = GetHeaderList();
            int colName = headersDict["Name"];
            int colQty = headersDict["Qty"];
            int colViewed = headersDict["Viewed"];
            int colCreated = headersDict["Created"];
            int colDirectLink = headersDict["Direct Link"];
            int colDelete = headersDict["Delete"];

            rows = driver.FindElements(By.XPath("//table[@class='table table-bordered']//tbody//tr"));

            for (int i = 1; i < rows.Count + 1; i++)
            {
                listOfWishlists.Add(new Additional.Wishlists(GetElement(i, colName), GetElement(i, colQty),
                    GetElement(i, colViewed), GetElement(i, colCreated),
                    GetElement(i, colDirectLink), GetElement(i, colDelete)));
            }

            return listOfWishlists;
        }

        public void OpenWishlist(string name)
        {
            IList<IWebElement> rows = null;

            rows = driver.FindElements(By.XPath("//table[@class='table table-bordered']//tbody//tr"));

            foreach (var td in rows)
            {
                var a = td.FindElement(By.TagName("a"));
                var href = a.GetAttribute("innerHTML");
                if (href.Contains(name))
                {
                    a.Click();
                    break;
                }
            }

            WaitUntilWishlistIsOpened();
        }

        public string GetProductName()
        {
            var getSettings =  SettingsReader.ReadSettings();   
            if (getSettings.Browser.Equals("firefox"))
            {
    
                IWebElement ProductName = driver.FindElement(By.CssSelector("p.product-name"));

                ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].scrollIntoView(true);", ProductName);
                Thread.Sleep(1000);
            }

            //var index = ProductName.Text.IndexOf("\r");
            return ProductName.Text;//.Substring(0, index);
        }

        private Dictionary<string, int> GetHeaderList()
        {
            Dictionary<string, int> headerList = new Dictionary<string, int>();
            IList<IWebElement> headers =
                driver.FindElements(By.XPath("//table[@class='table table-bordered']//thead//tr[1]/th"));
            int i = 1;
            foreach (var headerName in headers)
            {
                headerList.Add(headerName.Text, i);
                i++;
            }

            return headerList;
        }

        public void CloseConfirm(int secondsToWait = 15)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void WaitUntilTableIsVisible(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//table[@class='table table-bordered']")));
        }

        public void WaitUntilWishlistIsOpened(int secondsToWait = 2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.CssSelector("[id='showSendWishlist']")));
        }
    }

}