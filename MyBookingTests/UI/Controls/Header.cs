using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class Header : Control
    {
        public Header(IWebElement element) : base(element)
        {
        }
                
        private By _byLoginLink = By.CssSelector("li[class*='account_register_option'] div.sign_in_wrapper");
        private By _byLoginForm = By.CssSelector("body>div[class*='user-access-menu']");
        private By _byProfileLink = By.XPath(".//li[contains(@id, 'current_account')]/a");
        private By _byProfileMenu = By.CssSelector("div.profile-menu");
        private By _byLanguageLink = By.XPath(".//li[contains(@data-id, 'language_selector')]/a");
        private By _byLanguageMenu = By.XPath(".//div[contains(@class, 'language-content')]");
        private By _byCurrencyLink = By.XPath(".//li[contains(@data-id, 'currency_selector')]/a");
        private By _byCurrencyMenu = By.XPath(".//div[contains(@id, 'current_currency')]");

        public string ActualCurrency => WrappedElement.FindElement(_byCurrencyLink).GetAttribute("innerHTML");

        public LoginForm OpenLoginForm()
        {            
            WrappedElement.FindElement(_byLoginLink).Click();              
            return new LoginForm(Wait.Until(ExpectedConditions.ElementIsVisible(_byLoginForm)));
        }

        public ProfileMenu OpenProfileMenu()
        {
            WrappedElement.FindElement(_byProfileLink).Click();
            return new ProfileMenu(Wait.Until(ExpectedConditions.ElementIsVisible(_byProfileMenu)));
        }

        public LanguageMenu OpenLanguageMenu()
        {
            WrappedElement.FindElement(_byLanguageLink).Click();            
            return new LanguageMenu(Wait.Until(ExpectedConditions.ElementIsVisible(_byLanguageMenu)));
        }

        public CurrencyMenu OpenCurrencyMenu()
        {
            WrappedElement.FindElement(_byCurrencyLink).Click();            
            return new CurrencyMenu(Wait.Until(ExpectedConditions.ElementIsVisible(_byCurrencyMenu)));
        }

        public string GetFlagType()
        {
            WrappedElement = WrappedElement.FindElement(_byLanguageLink);
            string flag = WrappedElement.FindElement(By.CssSelector("img")).GetAttribute("alt");
            return flag.Equals("На русском") ? "Русский" : flag;
        }
    }
}
