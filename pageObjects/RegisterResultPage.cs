using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumNUnit.pageObjects
{
	public class RegisterResultPage
	{
        private IWebDriver driver;

        public RegisterResultPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); //This initializes all the page objects on this class to the driver
        }


        [FindsBy(How = How.CssSelector, Using = ".page-body .result")]
        private IWebElement successMsgElem;

        [FindsBy(How = How.CssSelector, Using = "a.ico-login")]
        private IWebElement login;

        public void goToLogin()
        {
            login.Click();
        }

        //public IWebElement getSuccessMsgElem()
        //{
        //    return successMsgElem;
        //}

        //public bool IsRegResultPageLoaded()
        //{
        //    try
        //    {
        //        //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //        //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".page-body .result")));
        //        return successMsgElem.Displayed;
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        return false;
        //    }

        //}

    }
}

