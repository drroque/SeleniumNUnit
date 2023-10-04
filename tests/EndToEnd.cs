using System.Collections.ObjectModel;
using System.Xml.Xsl;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumNUnit.pageObjects;
using SeleniumNUnit.util;
using WebDriverManager.DriverConfigs.Impl;

namespace EndToEnd
{
    public class EndToEnd : Base

    {
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("validEmail"), getDataParser().extractData("validPass"), getDataParser().extractDataArray("billingInfo"));
        }

        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("Positive")]
        public void TC09(String validEmail, String validPass, String[] billInfo)
        {

            String expectedPageURL = "https://demo.nopcommerce.com/";
            ProductCategoriesPage? pcPage;
            CheckoutPage? checkoutPage;

            HomePage homePage = new HomePage(getDriver());
            LoginPage loginPage = homePage.goToLogin();
            pcPage = loginPage.validLogin(validEmail, validPass);

            String actualPageURL = driver.Value.Url;

            // Register and Login
            if (actualPageURL == expectedPageURL)
            {

            }
            else
            {
                loginPage.goToRegister();
                RegisterPage regPage = homePage.goToRegister();
                RegisterResultPage regResultPage = regPage.validRegister(validEmail, validPass);
                regResultPage.goToLogin();
                pcPage = loginPage.validLogin(validEmail, validPass);
                actualPageURL = driver.Value.Url;

            }

            // Browse Notebooks
            pcPage.browseMacBook();
            

            string cartQtyText = pcPage.getCartQty();
            string cartQtyStr = cartQtyText.Replace("(", "").Replace(")", "");
            int cartQtyInt = Convert.ToInt32(cartQtyStr);

            // Add to Cart, Delete
            if (cartQtyInt > 0)
            {
                pcPage.getBtnShoppingCart().Click();
                pcPage.getBtnRemoveItem().Click();
                pcPage.browseMacBook();
                pcPage.getAddToCart().Click();
                pcPage.getBtnShoppingCart().Click();
            }
            else
            {
                pcPage.getAddToCart().Click();
                pcPage.getBtnShoppingCart().Click();
            }
            // Shopping Cart
            pcPage.getTerms().Click();
            checkoutPage = pcPage.goToCheckout();

            if (checkoutPage.getBillingEditBtn().Displayed)
            {
                checkoutPage.getBillingContinueBtn().Click();
            }
            else
            {
                checkoutPage.getBillCity().SendKeys(billInfo[0]);
                checkoutPage.getBillAddress1().SendKeys(billInfo[1]);
                checkoutPage.getBillZip().SendKeys(billInfo[2]);
                checkoutPage.getBillPhone().SendKeys(billInfo[3]);
                checkoutPage.selectFromDropdown1(billInfo[4]);
                checkoutPage.getBillingContinueBtn().Click();
            }

            checkoutPage.getShippingMethodContinueBtn().Click();
            checkoutPage.getPaymentMethodContinueBtn().Click();
            checkoutPage.getPaymentInfoContinueBtn().Click();
            checkoutPage.getConfirmOrderBtn().Click();

            Assert.That(checkoutPage.getThankYouMessage().Text, Is.EqualTo("Thank you"));

            checkoutPage.getLogoutBtn().Click();

            actualPageURL = driver.Value.Url;

            Assert.That(actualPageURL, Is.EqualTo(expectedPageURL));
        }

        [Test]
        public void AddToCartBasic()
        {

            // Find and click the "Computers" link
            IWebElement computers = driver.Value.FindElement(By.CssSelector(".top-menu.notmobile > li > a[href='/computers']"));
            computers.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement notebooksCategoryLink = driver.Value.FindElement(By.CssSelector(".sub-category-item h2.title a[href='/notebooks']"));
            notebooksCategoryLink.Click();



            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement macbookProduct = driver.Value.FindElement(By.CssSelector(".product-item[data-productid='4']"));
            macbookProduct.Click();

            String itemAdded = driver.Value.FindElement(By.CssSelector("div[class='product-name'] h1")).Text;
            TestContext.Progress.WriteLine(itemAdded);


            IWebElement addToCartButton = driver.Value.FindElement(By.CssSelector(".add-to-cart-button"));
            addToCartButton.Click();

            System.Threading.Thread.Sleep(2000);
            //Console.WriteLine("Order has been added to shopping cart.");

            // Click Shopping Cart
            IWebElement shoppingCart = driver.Value.FindElement(By.LinkText("shopping cart"));
            shoppingCart.Click();

            // Finding an item in a specific column excluding first row for a dynamic table --> overengineered
            IWebElement table = driver.Value.FindElement(By.CssSelector(".cart"));

            int desiredColumnIndex = 2;
            ReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            string itemFound = "";

            for (int rowIndex = 1; rowIndex < rows.Count; rowIndex++)   //rowIndex is 1 to exclude the first row (header) of the table 
            {
                var row = rows[rowIndex];
                ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));

                if (cells.Count > desiredColumnIndex)
                {
                    // Get the text of the cell in the desired column
                    string cellText = cells[desiredColumnIndex].Text;
                    // Console.WriteLine($"Item in column {desiredColumnIndex}: {itemText}");   //Check cellText

                    if (cellText == itemAdded)
                    //if (itemText == "just checking else")
                    {
                        itemFound = cells[desiredColumnIndex].Text;
                        break;
                    }
                    else
                    {
                        Assert.Fail("Item not found");
                    }
                }

            }

            Assert.That(itemFound, Is.EqualTo(itemAdded));

            Console.WriteLine($"Item in column {desiredColumnIndex}: {itemFound}");   //Check itemFound

            // Code for Extracting Shopping Caart Quantity (also excluding parenthesis)
            //IWebElement cartQtyWebElem = driver.FindElement(By.CssSelector(".cart-qty"));
            //String cartQtyStr = cartQtyWebElem.Text;
            //Console.WriteLine(cartQtyStr);
            //string val = cartQtyStr.Replace("(", "").Replace(")", "");
            //Console.WriteLine(val);
            //int intval = Convert.ToInt32(val);
            //Assert.Greater(intval, 0);

            //System.Threading.Thread.Sleep(2000);

            //CaptureScreenshot(driver, "ShoppingCart");

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
        //}

    }
}

