using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnit.pageObjects
{
	public class CheckoutPage
	{
        private IWebDriver driver;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            dropdown1 = new SelectElement(driver.FindElement(By.Id("BillingNewAddress_CountryId")));
            PageFactory.InitElements(driver, this); //This initializes all the page objects on this class to the driver
        }


        private SelectElement dropdown1;


        public void SelectOptionInDropdown1ByText(string optionText)
        {
            dropdown1.SelectByText(optionText);
        }

        public string GetSelectedOptionInDropdown1()
        {
            return dropdown1.SelectedOption.Text;
        }

        public List<string> GetOptionsInDropdown1()
        {
            List<string> options = new List<string>();
            foreach (var option in dropdown1.Options)
            {
                options.Add(option.Text);
            }
            return options;
        }

        [FindsBy(How = How.Id, Using = "BillingNewAddress_City")]
        private IWebElement bill_city;

        [FindsBy(How = How.Id, Using = "BillingNewAddress_Address1")]
        private IWebElement bill_address1;

        [FindsBy(How = How.Id, Using = "BillingNewAddress_ZipPostalCode")]
        private IWebElement bill_zip;

        [FindsBy(How = How.Id, Using = "BillingNewAddress_PhoneNumber")]
        private IWebElement bill_phone;

        [FindsBy(How = How.CssSelector, Using = "#edit-billing-address-button")]
        private IWebElement btnEditBilling;


        //[FindsBy(How = How.XPath, Using = "//button[@name='save' and @class='button-1 new-address-next-step-button")]
        [FindsBy(How = How.CssSelector, Using = "button[onclick='Billing.save()']")]
        private IWebElement btnContinueBilling;



        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'shipping-method-next-step-button')]")]
        private IWebElement btnContinueShippingMethod;


        [FindsBy(How = How.CssSelector, Using = "button.button-1.payment-method-next-step-button")]
        private IWebElement btnContinueMethodPayment;

        [FindsBy(How = How.XPath, Using = "//button[@class='button-1 payment-info-next-step-button']")]
        private IWebElement btnContinueInfoPayment;

        [FindsBy(How = How.CssSelector, Using = "button.button-1.confirm-order-next-step-button")]
        private IWebElement btnConfirmOrder;

        [FindsBy(How = How.XPath, Using = "//div[@class='page checkout-page order-completed-page']/div/h1")]
	private IWebElement thankYouMessage;

	[FindsBy(How = How.CssSelector, Using = "a[href='/logout']")]
	private IWebElement btnLogout;
        


        public IWebElement getBillCity()
        {
            return bill_city;
        }

        public IWebElement getBillAddress1()
        {
            return bill_address1;
        }

        public IWebElement getBillZip()
        {
            return bill_zip;
        }

        public IWebElement getBillPhone()
        {
            return bill_phone;
        }

        public void selectFromDropdown1(string optionText)
        {
            dropdown1.SelectByText(optionText);
        }

        public IWebElement getBillingEditBtn()
        {
            return btnEditBilling;
        }

        public IWebElement getBillingContinueBtn()
        {
            return btnContinueBilling;
        }


        public IWebElement getShippingMethodContinueBtn()
        {
            return btnContinueShippingMethod;
        }

        public IWebElement getPaymentMethodContinueBtn()
        {
            return btnContinueMethodPayment;
        }

        public IWebElement getPaymentInfoContinueBtn()
        {
            return btnContinueInfoPayment;
        }


        public IWebElement getConfirmOrderBtn()
        {
            return btnConfirmOrder;
        }

	public IWebElement getThankYouMessage() 
	{ 
    	    return thankYouMessage;
	}

	public IWebElement getLogoutBtn() 
	{
            return btnLogout;
	}
    }
}

