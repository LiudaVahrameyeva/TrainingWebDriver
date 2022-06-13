using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;


namespace TestProject2;

public class HomePage
{
    private readonly IWebDriver driver;
    private readonly string url = "https://www.yandex.com/";
    private IWebElement SearchField => driver.FindElement(By.XPath("//input[contains(@class,'input__control')]"));
    
    [FindsBy(How = How.ClassName, Using = "desk-notif-card__login-new-item-title")]
    private IWebElement Login { get; set; }
    
    public HomePage(IWebDriver browser)
    {
        this.driver = browser;
        PageFactory.InitElements(browser, this);
    }
    
    public void Navigate()
    {
        this.driver.Navigate().GoToUrl(this.url);
    }

    public void ClickLogin()
    {
        Login.Click();
    }
    
    public bool LoginIsDisplayed()
    {
        return Login.Displayed;
    }

    public void SetSearchValue(string searchValue)
    {
        SearchField.SendKeys(searchValue);
    }
}

