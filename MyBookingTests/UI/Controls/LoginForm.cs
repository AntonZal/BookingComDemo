using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class LoginForm : Control
    {
        public LoginForm(IWebElement element) : base(element)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = ".//input[@name='username']")]
        protected IWebElement Username;

        [FindsBy(How = How.XPath, Using = ".//input[@name='password']")]
        protected IWebElement Password;

        [FindsBy(How = How.CssSelector, Using = "a~div.clearfix~input[type=submit]")]
        protected IWebElement SubmitButton;

        public LoginForm FillLoginForm()
        {
            LoginData loginData = new LoginData();
            Username.SendKeys(loginData.Username);
            Password.SendKeys(loginData.Password);
            return this;
        }

        public void Submit()
        {
            SubmitButton.Click();
            Wait.Until(ExpectedConditions.StalenessOf(WrappedElement));
        }

        
    }
}
