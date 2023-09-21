using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumNUnit.util;
using WebDriverManager.DriverConfigs.Impl;

namespace Logout
{
    public class LogoutTest : Base

    {
        string Email = "aaa@test.com";
        string Password = "xTestPassword";


        [Test]
        public void Logout()
        {
            //Login User
            driver.FindElement(By.Id("Email")).SendKeys(Email);
            driver.FindElement(By.Id("Password")).SendKeys(Password);




            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement loginButton = driver.FindElement(By.CssSelector(".buttons .button-1.login-button"));
            loginButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine("Login successful.");

            // Logout User
            IWebElement logout = driver.FindElement(By.XPath("//a[@class='ico-logout']"));
            logout.Click();

            driver.FindElement(By.CssSelector("a.ico-register")).Click();

            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(500));
            //Console.WriteLine("Logout successful.");

            Boolean elemDisp = driver.FindElement(By.XPath("//a[@class='ico-login']")).Displayed;
            TestContext.Progress.Write(elemDisp);

            // Capture a screenshot after clicking the continue button
            //CaptureScreenshot(driver, "Logout");
        }

        // Method to capture a screenshot
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

        //    driver.Close();
        //}


    }
}

