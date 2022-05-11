using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections;
using System.Linq;
using System.Net.Mime;
using HtmlAgilityPack;

namespace TestProject2
{
    public class Task50Methods : Page 
    {
        public Task50Methods(WebDriver driver)
        {
            Driver = driver;

        }

        IWebElement Multiselect => Driver.FindElement(By.XPath("//select[@id = 'multi-select']")); 
        private IWebElement GetNewUser => Driver.FindElement(By.CssSelector(".btn"));
        private string FirstLastNameText =>  Driver.FindElement(By.XPath("//div[@id='loading']")).Text;

        private IWebElement GetAlertButton => Driver.FindElement(By.CssSelector("[onclick*='myAlertFunction']"));
        private IWebElement GetConfirmButton => Driver.FindElement(By.CssSelector("[onclick*='myConfirmFunction']"));
        private IWebElement GetPromptButton => Driver.FindElement(By.CssSelector("[onclick*='myPromptFunction']"));
        private IWebElement DownloadButton => Driver.FindElement(By.CssSelector("button[id=cricle-btn]"));
        private IWebElement PercentText => Driver.FindElement(By.XPath("//div[@class='percenttext']"));
        private IWebElement EntriesList => Driver.FindElement(By.CssSelector("[name=example_length]"));
        
        IWebElement TableData => Driver.FindElement(By.XPath("//table[@class='display dataTable no-footer']"));
        private IWebElement NextButton => Driver.FindElement(By.CssSelector("[class*='paginate_button next']"));
        

        public void GetNewUserClick()
        {
            GetNewUser.Click();
        }
        public bool FirstAndLastNameIsDisplayed()
        {
            return !string.IsNullOrEmpty(FirstLastNameText);
        }
        public void GetAlert()
        {
            GetAlertButton.Click();
        }
        public void GetConfirm()
        {
            GetConfirmButton.Click();
        }
        public void GetPrompt()
        {
            GetPromptButton.Click();
        }
        public void CloseAlert(int secondsToWait=15)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }
        public void CloseConfirm(int secondsToWait=15)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            Driver.SwitchTo().Alert();
            alert.Dismiss();
        }
        public void ClosePrompt(int secondsToWait=15)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.SendKeys("Selenium"); 
            alert.Accept();
        }
        public bool IsAlertPresented() 
        { 
            try 
            { 
                Driver.SwitchTo().Alert(); 
                return true; 
            }   
            catch (NoAlertPresentException Ex) 
            { 
                return false; 
            }   
        }
        public void ClickDownload()
        {
            DownloadButton.Click();
        }
        public int ConvertPercentToInt()
        {
            return Int32.Parse(PercentText.Text.Replace("%",""));
        }
        public void RefreshPageDuringDownloading()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
            wait.PollingInterval = TimeSpan.FromMilliseconds(300);
            wait.Until(e => ConvertPercentToInt()>=50);
            
            Driver.Navigate().Refresh();
        }
        public void SelectEntries(string quantity)
        {
            var selectElement = new SelectElement(EntriesList);
            
            selectElement.SelectByText(quantity);
        }
        public void ClickNextButton()
        {
           NextButton.Click();
        }
        public bool NextButtonIsEnabled()
        {
            return !NextButton.GetAttribute("class").Contains("disabled");
        }
        private string GetElement(int rowNum, int colNum)
        {
            return Driver.FindElement(By.XPath(
                $"//table[@class='display dataTable no-footer']//tbody//tr[{rowNum}]/td[{colNum}]")).Text;
        }
        public IList<Employee> GetList()
        {
            
            List<Employee> listOfEmployees = new List<Employee>();
            IList<IWebElement> rows = null;
            
            var headersDict = GetHeaderList();
            int colName = headersDict["Name"];
            int colPosition = headersDict["Position"];
            int colOffice = headersDict["Office"];
            int colAge = headersDict["Age"];
            int colSalary = headersDict["Salary"];

            while (NextButtonIsEnabled())
            {
                rows = Driver.FindElements(By.XPath("//table[@class='display dataTable no-footer']//tbody//tr"));
                
                for (int i = 1; i < rows.Count + 1; i++)
                {
                    listOfEmployees.Add(new Employee(GetElement(i, colName), GetElement(i, colPosition),
                        GetElement(i, colOffice), GetElement(i, colAge), GetElement(i, colSalary)));
                }

                ClickNextButton();
                
            }
             rows = Driver.FindElements(By.XPath("//table[@class='display dataTable no-footer']//tbody//tr"));
                
            for (int i = 1; i < rows.Count + 1; i++)
            {
                listOfEmployees.Add(new Employee(GetElement(i, colName), GetElement(i, colPosition),
                    GetElement(i, colOffice), GetElement(i, colAge), GetElement(i, colSalary)));
            }
            return listOfEmployees;
        }

        public IList<Employee> GetListOfEmployeesFiltered(int age, int salary)
        {
            List<Employee> filteredList = new List<Employee>();
            
            filteredList = GetList().Where(x => Int32.Parse(x.Age) > age && UpdateSalary(x.Salary) <= salary).ToList();
            
            return filteredList;
        }

        private int UpdateSalary(string salary)
        {
            string newSalary = salary.Replace("$","").Replace(",","").Replace("/y","");
            return Int32.Parse(newSalary.Trim());
        }

        private Dictionary<string, int> GetHeaderList()
        {
            Dictionary<string, int> headerList = new Dictionary<string, int>();
            IList<IWebElement>  headers = Driver.FindElements(By.XPath("//table[@class='display dataTable no-footer']//thead//tr[1]/th"));
            int i = 1;
            foreach (var headerName in headers)
            {
                headerList.Add(headerName.Text, i);
                i++;
            }

            return headerList;
        }
    }
}