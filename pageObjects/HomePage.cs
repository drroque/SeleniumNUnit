using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnit.pageObjects
{
	public class HomePage
	{
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
		{
            this.driver = driver;
            PageFactory.InitElements(driver, this); //This initializes all the page objects on this class to the driver
        }



        //Page Factory

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Register']")]
        private IWebElement register;

        //public IWebElement getRegister()
        //{
        //    return register;
        //}

        public RegisterPage goToRegister()
        {
            register.Click();
            return new RegisterPage(driver);
        }


    }
}

