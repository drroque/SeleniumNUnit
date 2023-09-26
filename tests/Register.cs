using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumNUnit.pageObjects;
using SeleniumNUnit.util;
using WebDriverManager.DriverConfigs.Impl;

namespace Register
{
    [Parallelizable(ParallelScope.Children)]
    public class RegisterTest : Base

    {


        [Test, Category("Positive")]
        public void TC01()
        {
            String expectedURL = "https://demo.nopcommerce.com/register?returnUrl=%2F";

            HomePage homePage = new HomePage(getDriver());
            

            //Click Register link
            homePage.goToRegister();
            //driver.FindElement(By.CssSelector("a.ico-register")).Click();
            
            IWebElement link = driver.Value.FindElement(By.LinkText("Register"));
            String hrefAttr = link.GetAttribute("href");
            //TestContext.Progress.WriteLine(hrefAttr);

            Assert.That(actual: hrefAttr, Is.EqualTo(expectedURL));
        }

        


        [Test, TestCaseSource("AddTestDataConfig"), Category("Negative")]
        [Parallelizable(ParallelScope.All)]
        public void TC02(String invalidEmail, String validPass)
        {
            //Data Driven Testing - Negative TC - Email Error
            HomePage homePage = new HomePage(getDriver());
            RegisterPage regPage = homePage.goToRegister();

            regPage.commonFillOuts();
            regPage.getEmail().SendKeys(invalidEmail);
            regPage.getPass().SendKeys(validPass);
            regPage.getCPass().SendKeys(validPass);
            regPage.getRegButton().Click();

            Boolean elemDisp = driver.Value.FindElement(By.Id("Email-error")).Displayed;
            Assert.IsTrue(elemDisp);

        }

        [Test, TestCaseSource("AddTestDataConfig"), Category("Negative")]
        public void TC03(String invalidEmail, String invalidPass)
        {
            //Negative TC - Password Error
            HomePage homePage = new HomePage(getDriver());
            RegisterPage regPage = homePage.goToRegister();

            regPage.commonFillOuts();
            regPage.getEmail().SendKeys(invalidEmail);
            regPage.getPass().SendKeys(invalidPass);
            regPage.getCPass().SendKeys(invalidPass);
            //regPage.getEmail().SendKeys("test@test.com");
            //regPage.getPass().SendKeys("12345");
            //regPage.getCPass().SendKeys("12345");
            regPage.getRegButton().Click();

            Boolean elemDisp = driver.Value.FindElement(By.Id("Password-error")).Displayed;
            Assert.IsTrue(elemDisp);
        }


        [Test, TestCaseSource("AddTestDataConfig2"), Category("Positive")]
        public void TC04(String validEmail, String validPass)
        {
            //String expectedMessage = "Your registration completed";
            String expectedPageURL = "https://demo.nopcommerce.com/registerresult/1?returnUrl=/";
            TestContext.Progress.WriteLine(expectedPageURL);


            HomePage homePage = new HomePage(getDriver());
            RegisterPage regPage = homePage.goToRegister();
            RegisterResultPage regResultPage = regPage.validRegister(validEmail, validPass);

            String actualPageURL = driver.Value.Url;

            Assert.That(actualPageURL, Is.EqualTo(expectedPageURL), "Page URL doesn't match");
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("invalidEmail"), getDataParser().extractData("validPass"));
            yield return new TestCaseData(getDataParser().extractData("invalidEmail"), getDataParser().extractData("invalidPass"));
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig2()
        {
            yield return new TestCaseData(getDataParser().extractData("validEmail"), getDataParser().extractData("validPass"));
        }


    }


}


// Run test in terminal
// dotnet test pathto.csproj (This commands runs all tests)
// dotnet test pathto.csproj --filter TestCategory=Positive
// dotnet test SeleniumNUnit.csproj --filter TestCategory=Positive -- TestRunParameters.Parameter\(name=\"browserName\", value=\"Chrome\"\)


//    try
//    {
//        // Find the success message element
//        IWebElement successMessage = driver.FindElement(By.CssSelector(".page-body .result"));

//        // Check if the success message is displayed
//        if (successMessage.Displayed)
//        {
//            Console.WriteLine("User registered successfully.");
//            CaptureScreenshot(driver, "RegisterSuccess");
//            System.Threading.Thread.Sleep(1000);
//        }
//    }
//    catch (NoSuchElementException)
//    {
//        // Find the error message element
//        IWebElement errorMessage = driver.FindElement(By.CssSelector(".message-error.validation-summary-errors"));

//        // Get the text of the error message
//        string messageText = errorMessage.Text;

//        // Print the error message
//        Console.WriteLine("Registration failed");
//        Console.WriteLine("Validation Message: " + messageText);
//        CaptureScreenshot(driver, "RegisterFailed");
//        System.Threading.Thread.Sleep(1000);
//    }
//}

//// Method to capture a screenshot
//static void CaptureScreenshot(IWebDriver driver, string registration)
//{
//    // Convert the driver to ITakesScreenshot
//    ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

//    // Capture the screenshot
//    Screenshot screenshot = screenshotDriver.GetScreenshot();

//    // Save the screenshot to a file
//    string screenshotPath = $"{registration}_{DateTime.Now:yyyyMMddHHmmss}.png";
//    screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

//    Console.WriteLine($"Screenshot saved as: {screenshotPath}");
//}

//[TearDown]
//public void CloseBrowser()
//{
//    TakeScreenshotDefaultImageFormat();

//    driver.Close();
//}



//public void TakeScreenshotDefaultImageFormat()
//{

//    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
//    var screenshotDirectoryPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "/Users/derik/Desktop/SeleniumNUnit");
//    var currentDate = DateTime.Now;
//    var filePath = $"{screenshotDirectoryPath}{TestContext.CurrentContext.Test.Name}_{currentDate.ToString("yyyy.MM.dd-HH.mm.ss")}.png";

//    if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
//    {

//        if (!Directory.Exists(screenshotDirectoryPath))
//        {
//            Directory.CreateDirectory(screenshotDirectoryPath);
//        }

//        screenshot.SaveAsFile(filePath);
//        TestContext.AddTestAttachment(filePath);
//}





//Select Date of Birth

//IWebElement dropdown = driver.FindElement(By.Name("DateOfBirthDay"));
//SelectElement select = new SelectElement(dropdown);
//select.SelectByValue("1");

//IWebElement dropdown2 = driver.FindElement(By.Name("DateOfBirthMonth"));
//SelectElement select2 = new SelectElement(dropdown2);
//select2.SelectByValue("1");

//IWebElement dropdown3 = driver.FindElement(By.Name("DateOfBirthYear"));
//SelectElement select3 = new SelectElement(dropdown3);
//select3.SelectByText("1998");

//regPage.commonFillOuts();
//regPage.SelectOptionInDropdown1ByText("1");
//regPage.SelectOptionInDropdown2ByText("February");
//regPage.SelectOptionInDropdown3ByText("2000");


//Enter Email
//driver.FindElement(By.Id("Email")).SendKeys(Email);

////Enter Password
//driver.FindElement(By.Id("Password")).SendKeys(validPW);
////Enter Confirm Password
//driver.FindElement(By.Id("ConfirmPassword")).SendKeys(validPW);


//// Find the button element by ID
//IWebElement registerButton = driver.FindElement(By.Id("register-button"));
//// Click on the "Register" button
//registerButton.Click();