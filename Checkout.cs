using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace Checkout
{
    public class CheckoutTest

    {
        IWebDriver driver;
        String getTitle;
        string Email = "test@test.com";
        string Password = "testtest";
        string City = "City";
        string Address1 = "Address1";
        string Address2 = "Address2";
        string ZipCode = "1234";
        string PhoneNumber = "09123456789";
        string FaxNumber = "5678";
        string Country = "Philippines";
        string Cardholder = "Cardholder Name";
        string CardNumber = "4111 1111 1111 1111";
        string Cardcode = "1234";
        
    
        [SetUp]
        public void StartBrowser()
        {
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
        public void Checkout()
        {
            //Login
            driver.FindElement(By.Id("Email")).SendKeys(Email);
            driver.FindElement(By.Id("Password")).SendKeys(Password);

            IWebElement loginButton = driver.FindElement(By.CssSelector(".buttons .button-1.login-button"));
            loginButton.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            // Go to Shopping Cart page
            IWebElement shoppingCart = driver.FindElement(By.Id("topcartlink"));
            shoppingCart.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

            //Select Gift Wrapping "Yes"
            IWebElement giftWrapping = driver.FindElement(By.Id("checkout_attribute_1"));
            SelectElement select = new SelectElement(giftWrapping);
            select.SelectByValue("2");

            //Click checkbox to agree terms
            driver.FindElement(By.Id("termsofservice")).Click();

            System.Threading.Thread.Sleep(3000);

            //Click Checkout button
            IWebElement checkoutButton = driver.FindElement(By.Id("checkout"));
            checkoutButton.Click();


        //Fill out Billing address
            IWebElement countryDropdown = driver.FindElement(By.Id("BillingNewAddress_CountryId"));
            string countryToSelect = (Country);
            // Select the desired country
            SelectElement selectElement = new SelectElement(countryDropdown);
            selectElement.SelectByText(countryToSelect);

            //Fill out State/Province
            IWebElement stateDropdown = driver.FindElement(By.Id("BillingNewAddress_StateProvinceId"));
            string stateToSelect = "Other";
            // Select the desired State/Province
            SelectElement selectElement2 = new SelectElement(stateDropdown);
            selectElement2.SelectByText(stateToSelect);

            //Fill out City
            driver.FindElement(By.Id("BillingNewAddress_City")).SendKeys(City);

            //Fill out Address 1
            driver.FindElement(By.Id("BillingNewAddress_Address1")).SendKeys(Address1);

            //Fill out Address 2
            driver.FindElement(By.Id("BillingNewAddress_Address2")).SendKeys(Address2);

            //Fill out Zip/Postal code
            driver.FindElement(By.Id("BillingNewAddress_ZipPostalCode")).SendKeys(ZipCode);

            //Fill out Phone no.
            driver.FindElement(By.Id("BillingNewAddress_PhoneNumber")).SendKeys(PhoneNumber);

            //Fill out Fax no.
            driver.FindElement(By.Id("BillingNewAddress_FaxNumber")).SendKeys(FaxNumber);

            System.Threading.Thread.Sleep(2);

            //Click Continue Button
            IWebElement continueBilling = driver.FindElement(By.XPath("//button[@name='save' and @class='button-1 new-address-next-step-button']"));
            continueBilling.Click();
            System.Threading.Thread.Sleep(2);

        //Fill out shipping method
            driver.FindElement(By.Id("shippingoption_1")).Click();

            //Continue Button
            IWebElement continueShipping = driver.FindElement(By.XPath("//button[contains(@class, 'shipping-method-next-step-button')]"));
            continueShipping.Click();
            System.Threading.Thread.Sleep(2);

         //Fill out payment method
            driver.FindElement(By.Id("paymentmethod_1")).Click();

            //Continue Button
            IWebElement continuePayment = driver.FindElement(By.CssSelector("button.button-1.payment-method-next-step-button"));
            continuePayment.Click();
            System.Threading.Thread.Sleep(2);

        //Fill out payment information
            IWebElement creditcardDropdown = driver.FindElement(By.Id("CreditCardType"));
            SelectElement select1 = new SelectElement(creditcardDropdown);
            select1.SelectByValue("MasterCard");

            driver.FindElement(By.Id("CardholderName")).SendKeys(Cardholder);
            driver.FindElement(By.Id("CardNumber")).SendKeys(CardNumber);

            IWebElement expireDate = driver.FindElement(By.Id("ExpireMonth"));
            SelectElement select2 = new SelectElement(expireDate);
            select2.SelectByValue("8");

            IWebElement expireYear = driver.FindElement(By.Id("ExpireYear"));
            SelectElement select3 = new SelectElement(expireYear);
            select3.SelectByValue("2030");

            driver.FindElement(By.Id("CardCode")).SendKeys(Cardcode);

            //Continue Button
            IWebElement continuePaymentInfo = driver.FindElement(By.CssSelector("button.button-1.payment-info-next-step-button"));
            continuePaymentInfo.Click();

            System.Threading.Thread.Sleep(2);

            //Order confirm button
            IWebElement confirmOrderButton = driver.FindElement(By.CssSelector("button.button-1.confirm-order-next-step-button"));
            confirmOrderButton.Click();

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Order has been successfully processed!");

            // Capture a screenshot after clicking the confirm button
            CaptureScreenshot(driver, "Checkout");
        }


        // Method to capture a screenshot
        static void CaptureScreenshot(IWebDriver driver, string screencapture)
        {
            // Convert the driver to ITakesScreenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            // Capture the screenshot
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            // Save the screenshot to a file
            string screenshotPath = Path.Combine($"{screencapture}_{DateTime.Now:yyyyMMddHHmmss}.png");
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

