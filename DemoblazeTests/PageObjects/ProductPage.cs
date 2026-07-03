using OpenQA.Selenium;

namespace DemoblazeTests.PageObjects
{
    public class ProductPage : BasePage
    {
        // #tbodyid es el contenedor donde Demoblaze pinta el producto.
        // Así evitamos agarrar h2/h3 ocultos de los modales.
        private By lblProductName = By.CssSelector("#tbodyid h2.name");
        private By lblProductPrice = By.CssSelector("#tbodyid h3.price-container");
        private By lblDescription = By.CssSelector("#more-information p");
        private By btnAddToCart = By.XPath("//a[text()='Add to cart']");

        public ProductPage(IWebDriver driver) : base(driver) { }

        // Espera hasta que el elemento EXISTA y además tenga texto (no vacío).
        // Esto es clave porque Demoblaze carga el detalle por JavaScript.
        private string WaitForText(By locator)
        {
            return Wait.Until(d =>
            {
                try
                {
                    string t = d.FindElement(locator).Text;
                    return string.IsNullOrEmpty(t) ? null : t;
                }
                catch { return null; }
            });
        }

        public string GetProductName() => WaitForText(lblProductName);
        public string GetProductPrice() => WaitForText(lblProductPrice);
        public string GetProductDescription() => WaitForText(lblDescription);

        public bool IsProductDetailComplete()
        {
            try
            {
                string name = GetProductName();
                string price = GetProductPrice();
                string desc = GetProductDescription();

                return !string.IsNullOrEmpty(name)
                    && !string.IsNullOrEmpty(price)
                    && !string.IsNullOrEmpty(desc);
            }
            catch { return false; }
        }

        public string AddToCart()
        {
            WaitForClickable(btnAddToCart).Click();
            return AcceptAlert();
        }
    }
}