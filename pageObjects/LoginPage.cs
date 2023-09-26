using System;
using System.Drawing;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnit.pageObjects
{
	public class LoginPage
	{
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); //This initializes all the page objects on this class to the driver
        }

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement email;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = ".buttons .button-1.login-button")]
        private IWebElement logInBtn;

        [FindsBy(How = How.CssSelector, Using = "a.ico-register")]
        private IWebElement registerLink;

        public IWebElement getEmail()
        {
            return email;
        }

        public IWebElement getPass()
        {
            return password;
        }


        public IWebElement getLogInButton()
        {
            return logInBtn;
        }

        public RegisterPage goToRegister()
        {
            registerLink.Click();
            return new RegisterPage(driver);
        }

        public void validLogin(String validEmail, String validPass)
        {
            getEmail().SendKeys(validEmail);
            getPass().SendKeys(validPass);
            getLogInButton().Click();
        }

    }
}

