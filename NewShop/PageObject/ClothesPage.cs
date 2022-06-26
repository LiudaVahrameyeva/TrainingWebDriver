using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NewShop.BaseSetup;
using NewShop.BaseSetup;
using NewShop.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V101.SystemInfo;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Size = System.Drawing.Size;

namespace NewShop.PageObject
{
    public class ClothesPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();
       

        public List<Additional.Products> AddItemsToWishlist(int addItems)
        {
            var getSettings =  SettingsReader.ReadSettings();
            
           var productImagesList = GetProductImagesList();
           var randomIndexes = GetListRandomInts(addItems, productImagesList.Count);
           List<Additional.Products> listOfProducts= new List<Additional.Products>();
           var topPanel = new TopPanelPage();
           
           foreach (var index in randomIndexes)
           {
              
               var prodDetail = new ProductDetailPage();

               IWebElement prodItem = driver.FindElement(By.CssSelector($"[src='{productImagesList[index]}']"));

               if (getSettings.Browser.Contains("firefox"))
               {
                   listOfProducts = ClickAddToWishlistFireFox(prodItem,index);
               }
               else
               {
                   listOfProducts = ClickAddToWishlistChrome(prodItem,index);
              
               }
               prodDetail.ClickCloseButton();
   
           }

           return listOfProducts;
        }
        
        private List<Additional.Products> ClickAddToWishlistFireFox(IWebElement prodItem, int index)
        {
            List<Additional.Products> listOfProducts= new List<Additional.Products>();
            ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].scrollIntoView(true);", prodItem);
            Actions action = new Actions(driver);
            action.MoveToElement(prodItem);
            Thread.Sleep(1500); 
            IWebElement prodName = driver.FindElement(By.XPath($"//a[@data-id-product='{index}']/ancestor::div[1]/preceding-sibling::h5/a"));
            listOfProducts.Add(new(prodName.Text));
            IWebElement addToWishlist = driver.FindElement(By.XPath($"//a[@class='addToWishlist wishlistProd_{index}']"));
            action.MoveToElement(addToWishlist);
            action.Click();
            action.Perform(); 
            return listOfProducts;
        }
        
        private List<Additional.Products> ClickAddToWishlistChrome(IWebElement prodItem, int index)
        {
            List<Additional.Products> listOfProducts= new List<Additional.Products>();
            Actions action = new Actions(driver);
            action.MoveToElement(prodItem).Perform();
            Thread.Sleep(1500);
            IWebElement prodName = driver.FindElement(By.XPath($"//a[@data-id-product='{index}']/ancestor::div[1]/preceding-sibling::h5/a"));
            listOfProducts.Add(new(prodName.Text));
            IWebElement addToWishlist = driver.FindElement(By.XPath($"//a[@class='addToWishlist wishlistProd_{index}']"));
            addToWishlist.Click();
            return listOfProducts;
        }
        
        public IList<Additional.Products> AddItemsToCard(int addItems)
        {
            var productImagesList = GetProductImagesList();
            var randomIndexes = GetListRandomInts(addItems, productImagesList.Count);
            List<Additional.Products> listOfProducts= new List<Additional.Products>();
            var getSettings =  SettingsReader.ReadSettings();

            foreach (var index in randomIndexes)
            {
                
                var prodDialog = new ProdDialogPage();
                IWebElement prodItem = driver.FindElement(By.CssSelector($"[src='{productImagesList[index]}']"));
                if (getSettings.Browser.Contains("firefox"))
                {
                    ClickAddToCardFireFox(prodItem,index);
                }
                else
                {
                    ClickAddToCardChrome(prodItem,index);
                   
                }
                Thread.Sleep(2000);
                listOfProducts.Add(new(prodDialog.GetProductName()));
                prodDialog.ClickContinueShopping();

            }

            return listOfProducts;
        }

        private void ClickAddToCardFireFox(IWebElement prodItem, int index)
        {
            ((IJavaScriptExecutor) driver).ExecuteScript("arguments[0].scrollIntoView(true);", prodItem);
            Actions action = new Actions(driver);
            action.MoveToElement(prodItem);
            Thread.Sleep(1500); 
            IWebElement addProdItem = driver.FindElement(By.XPath($"//a[@data-id-product='{index}' and not (@class='add_to_compare')]"));
            action.MoveToElement(addProdItem);
            action.Click();
            action.Perform(); 
        }
        
        private void ClickAddToCardChrome(IWebElement prodItem, int index)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(prodItem).Perform();
            Thread.Sleep(1500);
            IWebElement addProdItem = driver.FindElement(By.XPath($"//a[@data-id-product='{index}' and not (@class='add_to_compare')]"));
            addProdItem.Click();
        }

        private List<int> GetListRandomInts(int addItems, int amount)
        {
            Random rnd = new Random();
            var randomList = new List<int>();
            
            while (randomList.Count < addItems)
            {
                var number = rnd.Next(1, amount);
                if (!randomList.Contains(number))
                {
                    randomList.Add(number);
                }
            }

            return randomList;
        }
        
        public Dictionary< int, string> GetProductImagesList()
        {
        List<string>
            elementTexts = driver.FindElements(By.CssSelector("[class*='product_img_link'] img")).Select(iw 
                => iw.GetAttribute("src")).ToList();
        Dictionary<int, string> productImagesList = new Dictionary<int, string>();
        int i = 1;
        foreach (var element in elementTexts)
        {
            productImagesList.Add(i, element);
            i++;
        }

        return productImagesList;

        }
        
    }
}