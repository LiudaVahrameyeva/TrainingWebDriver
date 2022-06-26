using NewShop.BaseSetup;
using NewShop.BaseSetup;
using OpenQA.Selenium;

namespace NewShop.PageObject
{

    public class LoginPage
    {
        private IWebDriver driver = SetupWebDriver.GetDriver();

        private UserInfo userInfo = SettingsReader.ReadInfo();
        private IWebElement CreateEmail => driver.FindElement(By.CssSelector("[id='email_create']"));
        private IWebElement CreateAccountError => driver.FindElement(By.CssSelector("[id='create_account_error']"));
        private IWebElement SubmitButton => driver.FindElement(By.CssSelector("[id='SubmitCreate']"));
        private IWebElement Email => driver.FindElement(By.CssSelector("input[id = 'email']"));
        private IWebElement Password => driver.FindElement(By.CssSelector("input[id = 'passwd']"));
        private IWebElement SignInButton => driver.FindElement(By.CssSelector("[id = 'SubmitLogin']"));

        public void VerifyEmail(string email)
        {
            CreateEmail.SendKeys(email);
        }

        public bool CreateAccountErrorExists()
        {
            return CreateAccountError.Enabled;
        }

        public void ClickCreateAnAccount()
        {
            SubmitButton.Click();
        }

        public void EnterEmail(string email)
        {
            Email.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void ClickSignIn()
        {
            SignInButton.Click();
        }

        public void Login()
        {
            EnterEmail(userInfo.Login);
            EnterPassword(userInfo.Password);
            ClickSignIn();
        }
    }
}