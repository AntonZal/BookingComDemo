using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class LanguageMenu : Control
    {
        public LanguageMenu(IWebElement element) : base(element)
        {
        }

        protected IWebElement LanguageFor(string lang) => WrappedElement.FindElement(By.XPath($".//a[contains(., '{lang}')]"));

        public void SelectLanguage(string lang)
        {
            LanguageFor(lang).Click();
            Wait.Until(ExpectedConditions.StalenessOf(WrappedElement));
        }
    }
}
