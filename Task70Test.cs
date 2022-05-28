using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject2;

public class Task70Test
{
    public IWebDriver Driver { get; set; }

    [SetUp]
    public void SetupTest()
    {
        Driver = new ChromeDriver();
    }
    
    [Test]
    public void LoginToYa()
    {
        HomePage homePage = new HomePage(Driver);
        LoginYaPage loginYaPage = new LoginYaPage(Driver);
        LogoutYaPage logoutYaPage = new LogoutYaPage(Driver);
        
        homePage.Navigate();
        homePage.ClickLogin();
        
        loginYaPage.SetUserName("LiudaVahrameyeva");
        loginYaPage.ClickSubmit();
        loginYaPage.WaitUntilPasswordIsVisible();
        loginYaPage.SetPassword("Password#147!");
        loginYaPage.ClickSubmit();
        
        logoutYaPage.ClickAvatarIcon();
        logoutYaPage.ClickLogout();
        
        Assert.IsTrue(homePage.LoginIsDisplayed(), "Login button is not displayed.");
    }
    
    [TearDown]
    public void Cleanup()
    {
       Driver.Quit();
    }
   
}