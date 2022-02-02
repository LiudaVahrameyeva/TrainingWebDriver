using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    public class LoginPage : Page
    {

        public IWebElement MailEnter => Driver.FindElement(By.XPath("//div[contains(text(),'Войти')]"));
        public IWebElement FindButton => Driver.FindElement((By.XPath("//div[@class = 'search2__button']")));
        public IWebElement Login => Driver.FindElement(By.XPath("//input[@id = 'passp-field-login']"));
        public IWebElement Password => Driver.FindElement(By.XPath("//input[@id = 'passp-field-passwd']"));
        public IWebElement EnterButton => Driver.FindElement(By.XPath("//button[@id =  'passp:sign-in']"));

        public IWebElement Avatar =>
            Driver.FindElement(
                By.XPath("//div[contains(@class,'desk-notif-card')]//span[@class='avatar__image-wrapper']"));

        public LoginPage(WebDriver driver)
        {
            Driver = driver;

        }

        public WebDriver GetDriver()
        {
            return Driver;
        }

        public bool FindButtonIsDisplayed()
        {
            try
            {
                return FindButton.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void MailEnterClick()
        {
            MailEnter.Click();
        }

        public void SetLogin(string login)
        {
            Login.SendKeys(login);
        }
        
        public void SetPassword(string pass)
        {
            Password.SendKeys(pass);
        }

        public bool PasswordIsDisplayed()
        {
           
            try
            {
                return Password.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitUntilPasswordIsVisible(int secondsToWait)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//input[@id = 'passp-field-passwd']")));
        }
        
        public void WaitUntilAvatarIsVisible(int secondsToWait)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(e => e.FindElement(By.XPath("//div[contains(@class,'desk-notif-card')]//span[@class='avatar__image-wrapper']")));
        }
     
        public void ClickEnter()
        {
            EnterButton.Click();
        }
        
        
        
    }

}