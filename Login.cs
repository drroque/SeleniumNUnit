using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace Login
{
    public class LoginTest

    {
        IWebDriver driver;
        String getTitle;
        string Email = "aaa@test.com";
        string Password = "TestPassword";

        [SetUp]
        public void StartBroweser()
        {
            //Methods -geturl, click-
            //chromedriver.exe on chrome browser

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://demo.nopcommerce.com/login?returnUrl=%2F";
            TestContext.Progress.WriteLine(driver.Title + " - " + driver.Url);
            getTitle = Convert.ToString(driver.Title);
        }

        [Test]
        public void Login()
        {
            //Enter Email
            driver.FindElement(By.Id("Email")).SendKeys(Email);
            //Enter Password
            driver.FindElement(By.Id("Password")).SendKeys(Password);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement loginButton = driver.FindElement(By.CssSelector(".buttons .button-1.login-button"));
            loginButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            // Check for the error message
            IWebElement? errorMessage = null;

            try
            {
                errorMessage = driver.FindElement(By.CssSelector(".message-error.validation-summary-errors"));
            }
            catch (NoSuchElementException)
            {
                // No error message found, login might have succeeded
            }
            if (errorMessage != null)
            {
                Console.WriteLine("Login failed.");
                Console.WriteLine("Error Message: " + errorMessage.Text);
                CaptureScreenshot(driver, "LoginError");

            }
            else
            {
                Console.WriteLine("Login successful.");
                CaptureScreenshot(driver, "LoginSuccessful");

            }

        }
        // Method to capture a screenshot
        static void CaptureScreenshot(IWebDriver driver, string screencapture)
        {
            // Convert the driver to ITakesScreenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            // Capture the screenshot
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            // Save the screenshot to a file
            string screenshotPath = $"{screencapture}_{DateTime.Now:yyyyMMddHHmmss}.png";
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            Console.WriteLine($"Screenshot saved as: {screenshotPath}");
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}

