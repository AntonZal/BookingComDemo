using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class CurrencyMenu : Control
    {
        public CurrencyMenu(IWebElement element) : base(element)
        {
        }
        protected IWebElement CurrencyFor(string currencyISO) => WrappedElement.FindElements(By.XPath($".//a[contains(., '{currencyISO}')]")).FirstOrDefault();

        public void SelectCurrency(string currencyISO)
        {
            Wait.Until(dr => CurrencyFor(currencyISO)).Click();
            Wait.Until(ExpectedConditions.StalenessOf(WrappedElement));
        }
    }
}
