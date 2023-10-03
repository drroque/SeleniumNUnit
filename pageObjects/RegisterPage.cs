using System;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumNUnit.pageObjects
{
    public class RegisterPage
    {
        private IWebDriver driver;
        private SelectElement dropdown1;
        private SelectElement dropdown2;
        private SelectElement dropdown3;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            dropdown1 = new SelectElement(driver.FindElement(By.Name("DateOfBirthDay")));
            dropdown2 = new SelectElement(driver.FindElement(By.Name("DateOfBirthMonth")));
            dropdown3 = new SelectElement(driver.FindElement(By.Name("DateOfBirthYear")));
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Id, Using = "gender-female")]
        private IWebElement radioFemale;

        [FindsBy(How = How.Id, Using = "FirstName")]
        private IWebElement fName;

        [FindsBy(How = How.Id, Using = "LastName")]
        private IWebElement lName;

        [FindsBy(How = How.Id, Using = "Company")]
        private IWebElement company;

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement email;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "ConfirmPassword")]
        private IWebElement cPassword;

        [FindsBy(How = How.Id, Using = "register-button")]
        private IWebElement regBtn;


        public IWebElement getGenderF()
        {
            return radioFemale;
        }

        public IWebElement getFirstName()
        {
            return fName;
        }

        public IWebElement getLastName()
        {
            return lName;
        }

        public IWebElement getCompany()
        {
            return company;
        }

        public IWebElement getEmail()
        {
            return email;
        }

        public IWebElement getPass()
        {
            return password;
        }

        public IWebElement getCPass()
        {
            return cPassword;
        }

        public IWebElement getRegButton()
        {
            return regBtn;
        }

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

        public void SelectOptionInDropdown2ByText(string optionText)
        {
            dropdown2.SelectByText(optionText);
        }

        public string GetSelectedOptionInDropdown2()
        {
            return dropdown2.SelectedOption.Text;
        }

        public List<string> GetOptionsInDropdown2()
        {
            List<string> options = new List<string>();
            foreach (var option in dropdown2.Options)
            {
                options.Add(option.Text);
            }
            return options;
        }

        public void SelectOptionInDropdown3ByText(string optionText)
        {
            dropdown3.SelectByText(optionText);
        }

        public string GetSelectedOptionInDropdown3()
        {
            return dropdown3.SelectedOption.Text;
        }

        public List<string> GetOptionsInDropdown3()
        {
            List<string> options = new List<string>();
            foreach (var option in dropdown3.Options)
            {
                options.Add(option.Text);
            }
            return options;
        }

        public void commonFillOuts()
        {
            radioFemale.Click();
            fName.SendKeys("test");
            lName.SendKeys("test");
            company.SendKeys("test");
                        dropdown1.SelectByText("1");
            
            dropdown2.SelectByText("January");
            dropdown3.SelectByText("2000");
        }

        public RegisterResultPage validRegister(string userEmail, string pass)
        {
            radioFemale.Click();
            fName.SendKeys("test");
            lName.SendKeys("test");
            dropdown1.SelectByText("1");
            dropdown2.SelectByText("January");
            dropdown3.SelectByText("2000");
            company.SendKeys("test");
            email.SendKeys(userEmail);
            password.SendKeys(pass);
            cPassword.SendKeys(pass);
            regBtn.Click();
            return new RegisterResultPage(driver);
        }
    }
}

