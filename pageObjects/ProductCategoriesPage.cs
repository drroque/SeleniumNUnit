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


        //IWebElement computers = driver.FindElement(By.CssSelector(".top-menu.notmobile > li > a[href='/computers']"));
        //computers.Click();


        [FindsBy(How = How.CssSelector, Using = ".top-menu.notmobile > li > a[href='/computers']")]
        private IWebElement catComputers;

        [FindsBy(How = How.CssSelector, Using = ".sub-category-item h2.title a[href='/notebooks']")]
        private IWebElement subCatNotebooks;



    }
}

