using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TestProject2;

public class LogoutYaPage
{
    private readonly IWebDriver driver;
    
    [FindsBy(How = How.XPath, Using = "//div[contains(@class,'desk-notif-card')]//span[@class='avatar__image-wrapper']")]
    private IWebElement AvatarIcon { get; set; }
    
    [FindsBy(How = How.XPath, Using = "//a[@aria-label ='Выйти']")]
    private IWebElement Logout { get; set; }
    
    public LogoutYaPage(IWebDriver browser)
    {
        this.driver = browser;
        PageFactory.InitElements(browser, this);
    }
    
    public void ClickAvatarIcon()
    {
        AvatarIcon.Click();
    }
    
    public void ClickLogout()
    {
        Logout.Click();
    }
}