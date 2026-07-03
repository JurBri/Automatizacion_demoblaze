using OpenQA.Selenium;

namespace DemoblazeTests.PageObjects
{
    public class LoginPage : BasePage
    {
        private By tbxSignUpUsername = By.Id("sign-username");
        private By tbxSignUpPassword = By.Id("sign-password");
        private By btnSignUp = By.XPath("//button[text()='Sign up']");

        private By tbxLoginUsername = By.Id("loginusername");
        private By tbxLoginPassword = By.Id("loginpassword");
        private By btnLogin = By.XPath("//button[text()='Log in']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void WaitForSignUpModal()
        {
            Thread.Sleep(1000);
            WaitForClickable(tbxSignUpUsername);
        }

        public string DoSignUp(string username, string password)
        {
            WaitForClickable(tbxSignUpUsername).Clear();
            Driver.FindElement(tbxSignUpUsername).SendKeys(username);
            Driver.FindElement(tbxSignUpPassword).Clear();
            Driver.FindElement(tbxSignUpPassword).SendKeys(password);
            Driver.FindElement(btnSignUp).Click();
            return AcceptAlert();
        }

        public void WaitForLoginModal()
        {
            Thread.Sleep(1000);
            WaitForClickable(tbxLoginUsername);
        }

        public void DoLogin(string username, string password)
        {
            WaitForClickable(tbxLoginUsername).Clear();
            Driver.FindElement(tbxLoginUsername).SendKeys(username);
            Driver.FindElement(tbxLoginPassword).Clear();
            Driver.FindElement(tbxLoginPassword).SendKeys(password);
            Driver.FindElement(btnLogin).Click();
            Thread.Sleep(2000);
        }
    }
}