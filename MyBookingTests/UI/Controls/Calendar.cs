using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class Calendar : Control
    {
        public Calendar(IWebElement element) : base(element)
        {
        }

        protected IWebElement GetMonthYearElement(string monthYear) => WrappedElement.FindElement(By.XPath($".//table[contains(., '{monthYear}')]"));
        protected IReadOnlyCollection<IWebElement> Days => WrappedElement.FindElements(By.XPath($".//td[contains(@class, 'c2-day')]/span"));
        protected IWebElement NextMonth => WrappedElement.FindElement(By.CssSelector("div[class*=button-further]"));

        public void SelectDate(DateTime dateTime)
        {
            var monthYearString = dateTime.ToString("MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var dayString = dateTime.Day.ToString();
            while (!GetMonthYearElement(monthYearString).Displayed)
            {
                NextMonth.Click();
                // анимация..
                Thread.Sleep(300);
            }
            WrappedElement = GetMonthYearElement(monthYearString);
            Days.FirstOrDefault(el => el.Text.Trim().Equals(dayString)).Click();
            Wait.Until(dr => !WrappedElement.Displayed);
        }

        public void SelectDate(int date) 
        {
            var dateTime = DateTime.Now.AddDays(date);
            SelectDate(dateTime);
        }
    }
}
