using DemoblazeTests.PageObjects;
using NUnit.Framework;

namespace DemoblazeTests.Tests
{
    [TestFixture]
    public class DemoblazeTests : BaseTest
    {
        private const string TestUsername = "usuarioAutomacion01";
        private const string TestPassword = "Pass123!";

        [Test]
        public void Flujo1_CrearSesion()
        {
            var homePage = new HomePage(Driver);
            var loginPage = new LoginPage(Driver);

            extentTest.Info("Navegando a la página principal");
            homePage.GoTo();

            extentTest.Info("Abriendo modal de Sign Up");
            homePage.ClickSignUp();
            loginPage.WaitForSignUpModal();

            extentTest.Info("Registrando usuario");
            string alertSignUp = loginPage.DoSignUp(TestUsername, TestPassword);
            extentTest.Info($"Respuesta del registro: {alertSignUp}");

            extentTest.Info("Abriendo modal de Log In");
            homePage.ClickLogin();
            loginPage.WaitForLoginModal();

            extentTest.Info("Ingresando credenciales");
            loginPage.DoLogin(TestUsername, TestPassword);

            extentTest.Info("Validando mensaje de bienvenida");
            string welcomeText = homePage.GetWelcomeText();
            Assert.That(welcomeText, Does.Contain($"Welcome {TestUsername}"),
                $"Se esperaba 'Welcome {TestUsername}' pero se obtuvo '{welcomeText}'");

            Assert.That(homePage.IsLogoutVisible(), Is.True,
                "El botón Log out debería estar visible");
        }

        [Test]
        public void Flujo2_SeleccionCategoriaPhones()
        {
            var homePage = new HomePage(Driver);
            var productPage = new ProductPage(Driver);

            extentTest.Info("Navegando a la página principal");
            homePage.GoTo();

            extentTest.Info("Seleccionando categoría Phones");
            homePage.ClickPhones();

            extentTest.Info("Validando que aparece Samsung galaxy s6");
            Assert.That(homePage.IsProductVisible("Samsung galaxy s6"), Is.True,
                "Samsung galaxy s6 debería aparecer en Phones");

            extentTest.Info("Seleccionando el producto");
            homePage.ClickProduct("Samsung galaxy s6");

            extentTest.Info("Validando página de detalle");
            Assert.That(productPage.IsProductDetailComplete(), Is.True,
                "La página de detalle debe tener nombre, precio y descripción");

            extentTest.Info("Agregando al carrito");
            string alerta = productPage.AddToCart();
            Assert.That(alerta, Does.Contain("Product added"),
                "La alerta debe decir 'Product added'");
        }

        [Test]
        public void Flujo3_SeleccionCategoriaLaptops()
        {
            var homePage = new HomePage(Driver);
            var productPage = new ProductPage(Driver);

            extentTest.Info("Navegando a la página principal");
            homePage.GoTo();

            extentTest.Info("Seleccionando categoría Laptops");
            homePage.ClickLaptops();

            extentTest.Info("Validando que aparece Sony vaio i5");
            Assert.That(homePage.IsProductVisible("Sony vaio i5"), Is.True,
                "Sony vaio i5 debería aparecer en Laptops");

            extentTest.Info("Seleccionando el producto");
            homePage.ClickProduct("Sony vaio i5");

            extentTest.Info("Validando página de detalle");
            Assert.That(productPage.IsProductDetailComplete(), Is.True,
                "La página de detalle debe tener nombre, precio y descripción");

            extentTest.Info("Agregando al carrito");
            string alerta = productPage.AddToCart();
            Assert.That(alerta, Does.Contain("Product added"),
                "La alerta debe decir 'Product added'");
        }

        [Test]
        public void Flujo4_SeleccionCategoriaMonitors()
        {
            var homePage = new HomePage(Driver);
            var productPage = new ProductPage(Driver);

            extentTest.Info("Navegando a la página principal");
            homePage.GoTo();

            extentTest.Info("Seleccionando categoría Monitors");
            homePage.ClickMonitors();

            extentTest.Info("Validando que aparece Apple monitor 24");
            Assert.That(homePage.IsProductVisible("Apple monitor 24"), Is.True,
                "Apple monitor 24 debería aparecer en Monitors");

            extentTest.Info("Seleccionando el producto");
            homePage.ClickProduct("Apple monitor 24");

            extentTest.Info("Validando página de detalle");
            Assert.That(productPage.IsProductDetailComplete(), Is.True,
                "La página de detalle debe tener nombre, precio y descripción");

            extentTest.Info("Agregando al carrito");
            string alerta = productPage.AddToCart();
            Assert.That(alerta, Does.Contain("Product added"),
                "La alerta debe decir 'Product added'");
        }

        [Test]
        public void Flujo5_ProcesoCompletoCompra()
        {
            var homePage = new HomePage(Driver);
            var loginPage = new LoginPage(Driver);
            var productPage = new ProductPage(Driver);
            var cartPage = new CartPage(Driver);

            extentTest.Info("Iniciando sesión");
            homePage.GoTo();
            homePage.ClickLogin();
            loginPage.WaitForLoginModal();
            loginPage.DoLogin(TestUsername, TestPassword);

            extentTest.Info("Agregando Samsung galaxy s6 al carrito");
            homePage.ClickPhones();
            homePage.ClickProduct("Samsung galaxy s6");
            productPage.AddToCart();

            extentTest.Info("Navegando al carrito");
            homePage.GoTo();
            homePage.ClickCart();
            Thread.Sleep(2000);

            extentTest.Info("Validando que el producto está en el carrito");
            Assert.That(cartPage.IsProductInCart("Samsung galaxy s6"), Is.True,
                "Samsung galaxy s6 debe aparecer en el carrito");

            string total = cartPage.GetTotal();
            extentTest.Info($"Total del carrito: ${total}");
            Assert.That(total, Is.Not.Empty, "El total no debe estar vacío");

            extentTest.Info("Haciendo Place Order");
            cartPage.ClickPlaceOrder();

            extentTest.Info("Llenando formulario de compra");
            cartPage.FillOrderForm(
                name: "Emily Rojas",
                country: "Costa Rica",
                city: "San José",
                card: "4111111111111111",
                month: "6",
                year: "2025"
            );

            extentTest.Info("Validando confirmación de compra");
            string confirmacion = cartPage.GetConfirmationTitle();
            Assert.That(confirmacion, Does.Contain("Thank you for your purchase!"),
                "Debe aparecer el mensaje de confirmación");

            cartPage.ClickOk();
        }

        [Test]
        public void Flujo6_EliminacionProductosCarrito()
        {
            var homePage = new HomePage(Driver);
            var loginPage = new LoginPage(Driver);
            var productPage = new ProductPage(Driver);
            var cartPage = new CartPage(Driver);

            extentTest.Info("Iniciando sesión");
            homePage.GoTo();
            homePage.ClickLogin();
            loginPage.WaitForLoginModal();
            loginPage.DoLogin(TestUsername, TestPassword);

            extentTest.Info("Agregando Samsung galaxy s6");
            homePage.ClickPhones();
            homePage.ClickProduct("Samsung galaxy s6");
            productPage.AddToCart();

            extentTest.Info("Agregando Sony vaio i5");
            homePage.GoTo();
            homePage.ClickLaptops();
            homePage.ClickProduct("Sony vaio i5");
            productPage.AddToCart();

            extentTest.Info("Navegando al carrito");
            homePage.GoTo();
            homePage.ClickCart();
            Thread.Sleep(2000);

            extentTest.Info("Validando que ambos productos están en el carrito");
            Assert.That(cartPage.IsProductInCart("Samsung galaxy s6"), Is.True,
                "Samsung galaxy s6 debe estar en el carrito");
            Assert.That(cartPage.IsProductInCart("Sony vaio i5"), Is.True,
                "Sony vaio i5 debe estar en el carrito");

            extentTest.Info("Eliminando Samsung galaxy s6");
            cartPage.DeleteProduct("Samsung galaxy s6");
            Assert.That(cartPage.IsProductInCart("Samsung galaxy s6"), Is.False,
                "Samsung galaxy s6 ya no debe estar en el carrito");

            extentTest.Info("Eliminando Sony vaio i5");
            cartPage.DeleteProduct("Sony vaio i5");
            Assert.That(cartPage.IsProductInCart("Sony vaio i5"), Is.False,
                "Sony vaio i5 ya no debe estar en el carrito");
        }

        [Test]
        public void Flujo7_CierreSesion()
        {
            var homePage = new HomePage(Driver);
            var loginPage = new LoginPage(Driver);

            extentTest.Info("Iniciando sesión");
            homePage.GoTo();
            homePage.ClickLogin();
            loginPage.WaitForLoginModal();
            loginPage.DoLogin(TestUsername, TestPassword);

            extentTest.Info("Validando que la sesión está activa");
            string welcomeText = homePage.GetWelcomeText();
            Assert.That(welcomeText, Does.Contain($"Welcome {TestUsername}"),
                "Debe haber sesión activa antes de cerrar");

            extentTest.Info("Cerrando sesión");
            homePage.ClickLogout();
            Thread.Sleep(1500);

            extentTest.Info("Validando que la sesión se cerró");
            Assert.That(homePage.IsLoginVisible(), Is.True,
                "El botón Log In debe estar visible tras cerrar sesión");
            Assert.That(homePage.IsLogoutVisible(), Is.False,
                "El botón Log Out no debe estar visible tras cerrar sesión");
        }
    }
}