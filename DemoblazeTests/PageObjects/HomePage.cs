using OpenQA.Selenium;

namespace DemoblazeTests.PageObjects
{
    public class HomePage : BasePage
    {
        private readonly string url = "https://demoblaze.com/index.html";

        private By lnkSignUp = By.Id("signin2");
        private By lnkLogin = By.Id("login2");
        private By lnkLogout = By.Id("logout2");
        private By lnkCart = By.Id("cartur");
        private By lblWelcome = By.Id("nameofuser");

        private By lnkPhones = By.XPath("//a[text()='Phones']");
        private By lnkLaptops = By.XPath("//a[text()='Laptops']");
        private By lnkMonitors = By.XPath("//a[text()='Monitors']");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(url);
            Thread.Sleep(1500);
        }

        public void ClickSignUp() => WaitForClickable(lnkSignUp).Click();
        public void ClickLogin()
        {
            Thread.Sleep(1500);
            var btn = Driver.FindElement(lnkLogin);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", btn);
        }
        public void ClickLogout() => WaitForClickable(lnkLogout).Click();
        public void ClickCart() => WaitForClickable(lnkCart).Click();

        public string GetWelcomeText()
        {
            try
            {
                return Wait.Until(d =>
                {
                    var t = d.FindElement(lblWelcome).Text;
                    return string.IsNullOrEmpty(t) ? null : t;
                });
            }
            catch { return ""; }
        }

        public bool IsLogoutVisible()
        {
            try { return Driver.FindElement(lnkLogout).Displayed; }
            catch { return false; }
        }

        public bool IsLoginVisible()
        {
            try { return Driver.FindElement(lnkLogin).Displayed; }
            catch { return false; }
        }

        public void ClickPhones()
        {
            WaitForClickable(lnkPhones).Click();
            Thread.Sleep(1500);
        }

        public void ClickLaptops()
        {
            WaitForClickable(lnkLaptops).Click();
            Thread.Sleep(1500);
        }

        public void ClickMonitors()
        {
            WaitForClickable(lnkMonitors).Click();
            Thread.Sleep(1500);
        }

        public void ClickProduct(string productName)
        {
            WaitForClickable(By.LinkText(productName)).Click();
            Thread.Sleep(1000);
        }

        public bool IsProductVisible(string productName)
        {
            try
            {
                Wait.Until(d => d.FindElements(By.LinkText(productName)).Count > 0);
                return true;
            }
            catch { return false; }
        }
    }
}