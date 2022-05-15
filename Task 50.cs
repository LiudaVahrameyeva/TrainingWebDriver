
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.ReSharper.TestRunner.Abstractions.Extensions;
using Microsoft.VisualBasic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    public class Task_50
    {
        private static WebDriver driver;
        private Task50Methods _task50Methods => new Task50Methods(driver);
        
        [SetUp]
        public void Setup()
        {

        }
        

        [Test]
        public void MultiselectTest()
        { 
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/basic-select-dropdown-demo.html");
            IWebElement Multiselect = driver.FindElement(By.XPath("//select[@id = 'multi-select']")); 

            var expectedList = new List<string>();
            var selectElement = new SelectElement(Multiselect);
            var randomIndexes = GetListRandomInts();

           Actions action=new Actions(driver);
           action.KeyDown(Keys.Control).Build().Perform();
           
          foreach (var index in randomIndexes)
          {
              expectedList.Add(index.ToString());
              selectElement.SelectByIndex(index); 
          }

          action.KeyUp(Keys.Control).Build().Perform();

          var allSelectedOptions = selectElement.AllSelectedOptions.ToList();
          
          Assert.AreEqual(GetExpectedStateList(randomIndexes), ConvertToListOfStrings(allSelectedOptions), "List are not equals.");
        }

        [Test]
        public void WaitForUser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/dynamic-data-loading-demo.html");

            _task50Methods.GetNewUserClick();
            WaitUntilUserIsVisible(20);
           
            Assert.True(_task50Methods.FirstAndLastNameIsDisplayed(), "The First and Last name are not displayed yet.");

        }
        
        [Test]
        public void VerifyAlert()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/javascript-alert-box-demo.html");
            
            _task50Methods.GetAlert();
            _task50Methods.CloseAlert();
            
            Assert.False(_task50Methods.IsAlertPresented(), "Alerts should not be displayed.");
            
        }
        
        [Test]
        public void VerifyConfirm()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/javascript-alert-box-demo.html");

            _task50Methods.GetConfirm();
            _task50Methods.CloseConfirm();

            Assert.False(_task50Methods.IsAlertPresented(), "Alerts should not be displayed.");
        }
        
        [Test]
        public void VerifyPrompt()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/javascript-alert-box-demo.html");

            _task50Methods.GetPrompt();
            _task50Methods.ClosePrompt();
            
            Assert.False(_task50Methods.IsAlertPresented(), "Prompt should not be displayed.");
            
        }
        
        [Test]
        public void RefreshPage()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/bootstrap-download-progress-demo.html");

            _task50Methods.ClickDownload();
            _task50Methods.RefreshPageDuringDownloading();
            
            Assert.AreEqual(_task50Methods.ConvertPercentToInt(),0, "Page was not refreshed.");
        }
        
        [Test]
        public void GetListOfFilteredEmployees()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.seleniumeasy.com/table-sort-search-demo.html");

            _task50Methods.SelectEntries("10");
            var listResult = _task50Methods.GetListOfEmployeesFiltered(50, 450000);
            foreach (var item in listResult)
            {
                Console.WriteLine($"{item.Name}, {item.Office}, {item.Position}, {item.Age}, {item.Salary}");
            }
        }

        private List<int> GetListRandomInts()
        {
            Random rnd = new Random();
            var randomList = new List<int>();
            
            while (randomList.Count < 3)
            {
                var number = rnd.Next(0, 8);
                if (!randomList.Contains(number))
                {
                    randomList.Add(number);
                }
            }

            return randomList;
        }
        
        private List<string> GetExpectedStateList(List<int> randomList)
        {
            string[] stateList = new String[8]{"California", "Florida", "New Jersey", "New York","Ohio", "Texas", "Pennsylvania", "Washington"};
            var expectedStateList = new List<string>();

            foreach (var index in randomList)
            {
                expectedStateList.Add(stateList.GetValue(index).ToString());
            }

            expectedStateList = expectedStateList.OrderBy(x => x).ToList();
            
            return expectedStateList;
        }
        
        private List<string> ConvertToListOfStrings(List<IWebElement> actualWebElementList)
        {
          
            var actualList = new List<string>();

            foreach (WebElement element in actualWebElementList)
            {
                actualList.Add(element.Text);
            }

            actualList = actualList.OrderBy(x => x).ToList();
                
            return actualList;
        }
        
        public void WaitUntilUserIsVisible(int secondsToWait)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => !e.FindElement(By.XPath("//div[@id='loading']")).Text.Equals("loading..."));

        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}