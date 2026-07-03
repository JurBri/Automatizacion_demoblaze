using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DemoblazeTests.Genericos.DriversConfig;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoblazeTests.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        public static ExtentTest extentTest;
        public static ExtentReports extent;

        [OneTimeSetUp]
        public void StartReport()
        {
            var spark = new ExtentSparkReporter("Reporte.html");
            spark.Config.DocumentTitle = "Demoblaze - Sprint 1";
            spark.Config.ReportName = "Automatización Product Store";

            extent = new ExtentReports();
            extent.AttachReporter(spark);
            extent.AddSystemInfo("Ambiente", "https://demoblaze.com");
        }

        [SetUp]
        public void SetUp()
        {
            extentTest = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            Driver = ChromeFactory.CrearDriver(options);
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                extentTest.Pass("Test exitoso");
            }
            else
            {
                extentTest.Fail($"Test fallido {TestContext.CurrentContext.Result.Message}");
            }

            Driver?.Quit();
            Driver?.Dispose();
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }
    }
}