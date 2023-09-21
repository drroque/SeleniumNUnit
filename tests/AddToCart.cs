using System.Collections.ObjectModel;
using System.Xml.Xsl;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumNUnit.util;
using WebDriverManager.DriverConfigs.Impl;

namespace AddToCart
{
    public class AddToCartTest : Base

    {

        [Test]
        public void TC09()
        {

            // Find and click the "Computers" link
            IWebElement computers = driver.FindElement(By.CssSelector(".top-menu.notmobile > li > a[href='/computers']"));
            computers.Click();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement notebooksCategoryLink = driver.FindElement(By.CssSelector(".sub-category-item h2.title a[href='/notebooks']"));
            notebooksCategoryLink.Click();



            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement macbookProduct = driver.FindElement(By.CssSelector(".product-item[data-productid='4']"));
            macbookProduct.Click();

            String itemAdded = driver.FindElement(By.CssSelector("div[class='product-name'] h1")).Text;
            TestContext.Progress.WriteLine(itemAdded);


            IWebElement addToCartButton = driver.FindElement(By.CssSelector(".add-to-cart-button"));
            addToCartButton.Click();

            System.Threading.Thread.Sleep(2000);
            //Console.WriteLine("Order has been added to shopping cart.");

            // Click Shopping Cart
            IWebElement shoppingCart = driver.FindElement(By.LinkText("shopping cart"));
            shoppingCart.Click();

            // Finding an item in a specific column excluding first row for a dynamic table --> overengineered
            IWebElement table = driver.FindElement(By.CssSelector(".cart"));

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

