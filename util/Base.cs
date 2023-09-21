using System;
using System.Configuration;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SeleniumNUnit.pageObjects;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumNUnit.util
{
	public class Base
	{
        public ExtentReports extent;
        public ExtentTest test;
        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "/index.html";

            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local Host");
            extent.AddSystemInfo("Environment", "Test");
        }


        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();



        [SetUp]        
        public void StartBrowser()
        {

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);  //Create Test Node per Test
            String browserName = ConfigurationManager.AppSettings["browser"];
            // Alternative 1:
            //if (ConfigurationManager.AppSettings["browser"] != null)
            //{
            //    browserName = ConfigurationManager.AppSettings["browser"];
            //}
            //else
            //{
            //    throw new Exception("Configuration setting 'browser' not found.");
            //}

            //Alternative 2:If browser is null, then use Chrome
            //String browserName = ConfigurationManager.AppSettings["browser"] ?? "Chrome";

            InitBrowser(browserName);


            driver.Value.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://demo.nopcommerce.com";
            //driver.Url = "https://demo.nopcommerce.com/register?returnUrl=%2F";
            //TestContext.Progress.WriteLine(driver.Title + " - " + driver.Url); 
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }



        public void InitBrowser(string browserName)
        {

            switch (browserName)
            {

                case "Firefox":

                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":

                    driver.Value = new EdgeDriver();
                    break;

                default:
                    throw new Exception("Unsupported browser specified in the configuration");
            }

        }

        public static jsonReader getDataParser()
        {
            return new jsonReader();
        }


        [TearDown]
        public void AfterTest()
        {
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test have Failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "Test failed with stack trace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {

            }
            extent.Flush(); // instructs ExtentReports write the test information to a destination
            
            driver.Value.Quit();
        }

        public Media captureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}

