using System;
using OpenQA.Selenium;
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
    }
}

