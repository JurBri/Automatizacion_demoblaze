using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoblazeTests.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement WaitForElement(By locator)
        {
            return Wait.Until(d => d.FindElement(locator));
        }

        protected IWebElement WaitForClickable(By locator)
        {
            return Wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return (el.Displayed && el.Enabled) ? el : null;
            });
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        protected string AcceptAlert()
        {
            Wait.Until(d =>
            {
                try { d.SwitchTo().Alert(); return true; }
                catch { return false; }
            });
            var alert = Driver.SwitchTo().Alert();
            string texto = alert.Text;
            alert.Accept();
            return texto;
        }
    }
}