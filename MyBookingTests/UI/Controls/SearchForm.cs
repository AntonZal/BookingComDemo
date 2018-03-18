using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class SearchForm : Control
    {
        public SearchForm(IWebElement element) : base(element)
        {
        }

        private By _byDestinationField = By.CssSelector("input[class*=destination]");
        private By _byDateFields = By.CssSelector("div[class*=date-field__wrapper]");
        private By _byArrivalCalendar = By.XPath(".//div[contains(@class, 'calendar-header') and contains(., 'Check-in')]/..");
        private By _byDepartureCalendar = By.XPath(".//div[contains(@class, 'calendar-header') and contains(., 'Check-out')]/..");
        private By _bySubmit = By.CssSelector("button[type=submit]");
        private By _byDestinationError = By.CssSelector("div#destination__error");
        private By _byDurationError = By.XPath(".//div[@role='alert']/div[contains(., 'more than 30 nights')]");
        private By _byDroppedList = By.CssSelector("ul[role=listbox]");
        private By _byDestinationError2 = By.CssSelector("div[class='sb-searchbox__error -visible']");

        private DroopedList _droppedList => new DroopedList(Driver.FindElement(_byDroppedList));

        public IWebElement DestinationField => WrappedElement.FindElement(_byDestinationField);

        public List<IWebElement> DestinationErrors()
        {
            var list = WrappedElement.FindElements(_byDestinationError).ToList();
            list.AddRange(WrappedElement.FindElements(_byDestinationError2).ToList());
            return list;
        }
        
        public IWebElement DurationError => WrappedElement.FindElement(_byDurationError);
        
        public Calendar OpenArrivalCalendar(bool destination)
        {
            if (!Driver.FindElements(_byArrivalCalendar).Any(el => el.Displayed))  //After filling destination field, arrivalCalendar is autoOpened
            {    
                WrappedElement.FindElements(_byDateFields).First().Click();
            }
            Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_byArrivalCalendar));
            return new Calendar(Driver.FindElement(_byArrivalCalendar));
        }

        public Calendar OpenDepartureCalendar()
        {
            WrappedElement.FindElements(_byDateFields).Last().Click();
            Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_byDepartureCalendar));
            return new Calendar(Driver.FindElement(_byDepartureCalendar));
        }

        public void Submit()
        {
            WrappedElement.FindElement(_bySubmit).Click();
        }
        
        public SearchForm SendSearchForm(bool destination, bool nearestWeekend, int duration, bool remainingFieldsByDefault)
        {
            var searchData = DataHelper.GenerateSearchData(destination, nearestWeekend, duration, remainingFieldsByDefault);

            DestinationField.Clear();
            DestinationField.SendKeys(searchData.Locality);

            if (destination)  
            {
                DroopedListHandling();
            }

            OpenArrivalCalendar(destination).SelectDate(searchData.ArrivalDate);
            OpenDepartureCalendar().SelectDate(searchData.DepartureDate);

            return this;
        }

        public void DroopedListHandling()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(_byDroppedList));
           _droppedList.SelectSomeItem();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_byDroppedList));
        }
    }
}
