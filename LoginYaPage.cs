using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace TestProject2;

public class LoginYaPage
{
    private readonly IWebDriver driver;
    
    [FindsBy(How = How.Id, Using = "passp-field-login")]
    private IWebElement UserName { get; set; }

    [FindsBy(How = How.Id, Using = "passp-field-passwd")]
    private IWebElement Password { get; set; }

    [FindsBy(How = How.Id, Using = "passp:sign-in")]
    private IWebElement Submit { get; set; }
    
    public LoginYaPage(IWebDriver browser)
    {
        this.driver = browser;
        PageFactory.InitElements(browser, this);
    }

    public void ClickSubmit()
    {
        Submit.Click();
    }
    
    public void SetUserName(string userName)
    {
        UserName.SendKeys(userName);
    }
    
    public void SetPassword(string pass)
    {
        Password.SendKeys(pass);
    }
    
    public void WaitUntilPasswordIsVisible(int secondsToWait = 15)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsToWait));
        wait.Until(e => e.FindElement(By.XPath("//input[@id = 'passp-field-passwd']")));
    }
}