using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnit.pageObjects
{
	public class ProductCategoriesPage
	{
        private IWebDriver driver;

        public ProductCategoriesPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); //This initializes all the page objects on this class to the driver
        }


        [FindsBy(How = How.CssSelector, Using = ".top-menu.notmobile > li > a[href='/computers']")]
        private IWebElement catComputers;

        [FindsBy(How = How.CssSelector, Using = ".sub-category-item h2.title a[href='/notebooks']")]
        private IWebElement subCatNotebooks;

        [FindsBy(How = How.CssSelector, Using = ".product-item[data-productid='4']")]
        private IWebElement productMacbook;

        [FindsBy(How = How.XPath, Using = "//span[@class='cart-qty']")]
        private IWebElement cartQtyRaw;

        [FindsBy(How = How.CssSelector, Using = ".add-to-cart-button")]
        private IWebElement btnAddToCart;

        [FindsBy(How = How.CssSelector, Using = ".ico-cart")]
        private IWebElement btnShoppingCart;

        [FindsBy(How = How.XPath, Using = "//button[@class='remove-btn']")]
        private IWebElement btnRemoveItem;

        [FindsBy(How = How.Id, Using = "termsofservice")]
        private IWebElement terms;

        [FindsBy(How = How.Id, Using = "checkout")]
        private IWebElement btnCheckout;



        public IWebElement getCatComputers()
        {
            return catComputers;
        }

        public IWebElement getSubCatNotebooks()
        {
            return subCatNotebooks;
        }

        public IWebElement getProductMacbook()
        {
            return productMacbook;
        }

        public String getCartQty()
        {
            String cartQty = cartQtyRaw.Text;
           

            return cartQty;
        }


        public IWebElement getAddToCart()
        {
            return btnAddToCart;
        }

        public IWebElement getBtnShoppingCart()
        {
            return btnShoppingCart;
        }

        public IWebElement getBtnRemoveItem()
        {
            return btnRemoveItem;
        }

        public void browseMacBook()
        {
            getCatComputers().Click();
            getSubCatNotebooks().Click();
            getProductMacbook().Click();
        }

        public IWebElement getTerms()
        {
            return terms;
        }


        public CheckoutPage goToCheckout()
        {
            btnCheckout.Click();
            return new CheckoutPage(driver);
        }


    }
}

