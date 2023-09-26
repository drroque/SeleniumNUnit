using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumNUnit.pageObjects;
using SeleniumNUnit.util;
using WebDriverManager.DriverConfigs.Impl;

namespace Login
{
    public class LoginTest : Base
    {

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("validEmail"), getDataParser().extractData("validPass"));
        }

        [Test, Category("Positive")]
        public void TC05()
        {
            String expectedPageURL = "https://demo.nopcommerce.com/login?returnUrl=%2F";
            HomePage homePage = new HomePage(getDriver());
            LoginPage loginPage = homePage.goToLogin();

            String actualPageURL = driver.Value.Url;
            Assert.That(actualPageURL, Is.EqualTo(expectedPageURL), "Page URL doesn't match");

        }

        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("Positive")]
        public void TC06(String validEmail, String validPass)
        {


            String expectedPageURL = "https://demo.nopcommerce.com/";

            HomePage homePage = new HomePage(getDriver());
            LoginPage loginPage = homePage.goToLogin();
            loginPage.validLogin(validEmail, validPass);

            String actualPageURL = driver.Value.Url;
            //Assert.That(actualPageURL, Is.EqualTo(expectedPageURL), "Page URL doesn't match");


            if (actualPageURL == expectedPageURL)
            {
                Assert.Pass();
            }
            else
            {
                loginPage.goToRegister();
                RegisterPage regPage = homePage.goToRegister();
                RegisterResultPage regResultPage = regPage.validRegister(validEmail, validPass);
                regResultPage.goToLogin();
                loginPage.validLogin(validEmail, validPass);
                actualPageURL = driver.Value.Url;

                Assert.That(actualPageURL, Is.EqualTo(expectedPageURL), "Page URL doesn't match");
            }
            
        }


        //[Test]
        //public void TC07()
        //{

        //}


        //IWebDriver driver;
        //String getTitle;
        //string Email = "aaa@test.com";
        //string Password = "TestPassword";

        //[Test]
        //public void LoginBasic()
        //{

        //    //Enter Email
        //    driver.FindElement(By.Id("Email")).SendKeys(Email);

        //    //Enter Password
        //    driver.FindElement(By.Id("Password")).SendKeys(Password);

        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

        //    IWebElement loginButton = driver.FindElement(By.CssSelector(".buttons .button-1.login-button"));
        //    loginButton.Click();

        //    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

        //    //Check for the error message
        //    IWebElement? errorMessage = null;

        //    try
        //    {
        //        errorMessage = driver.FindElement(By.CssSelector(".message-error.validation-summary-errors"));
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        // No error message found, login might have succeeded
        //    }
        //    if (errorMessage != null)
        //    {
        //        Console.WriteLine("Login failed.");
        //        Console.WriteLine("Error Message: " + errorMessage.Text);
        //        CaptureScreenshot(driver, "LoginError");

        //    }
        //    else
        //    {
        //        Console.WriteLine("Login successful.");
        //        CaptureScreenshot(driver, "LoginSuccessful");
        //    }
        //}
        ////Method to capture a screenshot
        //static void CaptureScreenshot(IWebDriver driver, string screencapture)
        //{
        //    // Convert the driver to ITakesScreenshot
        //    ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

        //    // Capture the screenshot
        //    Screenshot screenshot = screenshotDriver.GetScreenshot();

        //    // Save the screenshot to a file
        //    string screenshotPath = $"{screencapture}_{DateTime.Now:yyyyMMddHHmmss}.png";
        //    screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

        //    Console.WriteLine($"Screenshot saved as: {screenshotPath}");
        //}

    }
}

