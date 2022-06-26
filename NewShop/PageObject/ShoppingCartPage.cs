using System;
using System.Collections.Generic;
using System.Linq;
using NewShop.BaseSetup;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NewShop.PageObject
{

    public class ShoppingCartPage
    {
        private IWebDriver driver= SetupWebDriver.GetDriver();
        private IWebElement ProductQuantity => driver.FindElement(By.CssSelector("[id='summary_products_quantity']"));

        public int GetQuantity()
        {
            var q = ProductQuantity.Text;
            q = q.Replace("Products","").Trim();
            return int.Parse(q);
        }
        
        public IList<Additional.Products> GetList()
        {
            WaitUntilTableIsVisible();
            List<Additional.Products> listOfProducts = new List<Additional.Products>();
            IList<IWebElement> rows = null;

            rows = driver.FindElements(By.XPath("//table[@class='table table-bordered stock-management-on']//tbody//tr"));

            for (int i = 1; i < rows.Count + 1; i++)
            {
                var product = GetElement(i, 2);
                listOfProducts.Add(new Additional.Products(product.Substring(0, product.IndexOf("\r"))));
            }

            return listOfProducts;
        }
    
        private string GetElement(int rowNum, int colNum)
        {
            return driver.FindElement(By.XPath(
                $"//table[@class='table table-bordered stock-management-on']//tbody//tr[{rowNum}]/td[{colNum}]")).Text;
        }

        public bool ListCompare(List<Additional.Products> l, List<Additional.Products> l2)
        {
            return l.SequenceEqual(l2);
        }

        public void WaitUntilTableIsVisible(int secondsToWait=2000)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//table[@class='table table-bordered stock-management-on']")));
        }
    }
}