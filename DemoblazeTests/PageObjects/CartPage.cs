using OpenQA.Selenium;

namespace DemoblazeTests.PageObjects
{
    public class CartPage : BasePage
    {
        private By lblTotal = By.Id("totalp");
        private By btnPlaceOrder = By.XPath("//button[text()='Place Order']");

        private By tbxOrderName = By.Id("name");
        private By tbxOrderCountry = By.Id("country");
        private By tbxOrderCity = By.Id("city");
        private By tbxOrderCard = By.Id("card");
        private By tbxOrderMonth = By.Id("month");
        private By tbxOrderYear = By.Id("year");
        private By btnPurchase = By.XPath("//button[text()='Purchase']");

        private By lblConfirmation = By.CssSelector(".sweet-alert h2");
        private By lblOrderDetails = By.CssSelector(".sweet-alert p.lead");
        private By btnOk = By.XPath("//button[text()='OK']");

        public CartPage(IWebDriver driver) : base(driver) { }

        public bool IsProductInCart(string productName)
        {
            try
            {
                Thread.Sleep(1000);
                var filas = Driver.FindElements(
                    By.XPath($"//td[text()='{productName}']"));
                return filas.Count > 0;
            }
            catch { return false; }
        }

        public string GetTotal()
        {
            return WaitForElement(lblTotal).Text;
        }

        public void DeleteProduct(string productName)
        {
            By deleteBy = By.XPath($"//td[text()='{productName}']" +
                                    "/following-sibling::td/a[text()='Delete']");

            // Espera a que el enlace Delete de ESE producto exista
            // (importante: después de eliminar un producto, la tabla se recarga)
            Wait.Until(d => d.FindElements(deleteBy).Count > 0);

            Driver.FindElement(deleteBy).Click();
            Thread.Sleep(2000); // Espera que la tabla se actualice tras eliminar
        }

        public void ClickPlaceOrder()
        {
            WaitForClickable(btnPlaceOrder).Click();
            Thread.Sleep(1000);
        }

        public void FillOrderForm(string name, string country, string city,
                                   string card, string month, string year)
        {
            WaitForElement(tbxOrderName).Clear();
            Driver.FindElement(tbxOrderName).SendKeys(name);
            Driver.FindElement(tbxOrderCountry).SendKeys(country);
            Driver.FindElement(tbxOrderCity).SendKeys(city);
            Driver.FindElement(tbxOrderCard).SendKeys(card);
            Driver.FindElement(tbxOrderMonth).SendKeys(month);
            Driver.FindElement(tbxOrderYear).SendKeys(year);
            Driver.FindElement(btnPurchase).Click();
            Thread.Sleep(2000);
        }

        public string GetConfirmationTitle()
        {
            return WaitForElement(lblConfirmation).Text;
        }

        public string GetOrderDetails()
        {
            return WaitForElement(lblOrderDetails).Text;
        }

        public void ClickOk()
        {
            WaitForClickable(btnOk).Click();
        }
    }
}