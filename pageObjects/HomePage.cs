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

        [FindsBy(How = How.CssSelector, Using = "a.ico-login")]
        private IWebElement login;

        public RegisterPage goToRegister()
        {
            register.Click();
            return new RegisterPage(driver);
        }

        public LoginPage goToLogin()
        {
            login.Click();
            return new LoginPage(driver);
        }




    }
}

