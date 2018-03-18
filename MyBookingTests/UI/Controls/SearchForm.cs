using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyBookingTests.Entities;
using MyBookingTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace MyBookingTests.UI.Controls
{
    public class SearchForm : Control
    {
        public SearchForm(IWebElement element) : base(element)
        {
        }

        private By _byDestinationField = By.CssSelector("input[class*=destination]");
        private By _byDateFields = By.CssSelector("div[class*=date-field__wrapper], button[class*=date-field]");
        private By _byArrivalCalendar = By.XPath(".//div[contains(@class, 'calendar-header') and contains(., 'Check-in')]/..");
        private By _byDepartureCalendar = By.XPath(".//div[contains(@class, 'calendar-header') and contains(., 'Check-out')]/..");
        private By _bySubmit = By.CssSelector("button[type=submit]");
        private By _byDestinationError = By.CssSelector("div#destination__error");
        private By _byDurationError = By.XPath(".//div[@role='alert']/div[contains(., 'more than 30 nights')]");
        private By _byDroppedList = By.CssSelector("ul[role=listbox], ul[class*=autocomplete__list]");
        private By _byDestinationError2 = By.CssSelector("div[class='sb-searchbox__error -visible']");

        private DroopedList _droppedList => new DroopedList(Driver.FindElement(_byDroppedList));

        public IWebElement DestinationField => WrappedElement.FindElement(_byDestinationField);
        public IWebElement DurationError => WrappedElement.FindElement(_byDurationError);


        internal SearchForm FillForm(Table table)  //for specflow table
        {
            var header = table.Header;
            var row = table.Rows.First();

            if (header.Contains("enter location"))
            {
                WrappedElement.FindElement(_byDestinationField).SendKeys(row["enter location"]);
            }
            if (header.Contains("select location") && !string.IsNullOrWhiteSpace(row["select location"]))
            {                
                DroopedListHandling(row["select location"]);
            }
            if (header.Contains("checkin") )
            {
                OpenArrivalCalendar(true).SelectDate(Int32.Parse(row["checkin"])); // date here is the numbers of a days from today
            }
            if (header.Contains("checkout"))
            {
                OpenDepartureCalendar().SelectDate(Int32.Parse(row["checkout"]));  // date here is the numbers of a days from today
            }
            // далее можно добавить обработку полей для всех элементов формы
            return this;
        }

        internal SearchForm FillForm(SearchFormData data)   //for specflow   table.CreateInstance
        {  
            if (!string.IsNullOrWhiteSpace(data.EnterLocation))
            {
                DestinationField.Clear();
                DestinationField.SendKeys(data.EnterLocation);
            }
            if (!string.IsNullOrWhiteSpace(data.SelectLocation))
            {
                DroopedListHandling(data.SelectLocation);
            }
            if (!string.IsNullOrWhiteSpace(data.Checkin))
            {
                OpenArrivalCalendar(true).SelectDate(Int32.Parse(data.Checkin));
            }
            if (!string.IsNullOrWhiteSpace(data.Checkout))
            {
                OpenDepartureCalendar().SelectDate(Int32.Parse(data.Checkout));
            }
            // далее можно добавить обработку полей для всех элементов формы
            return this;
        }        

        public List<IWebElement> DestinationErrors()
        {
            var list = WrappedElement.FindElements(_byDestinationError).ToList();
            list.AddRange(WrappedElement.FindElements(_byDestinationError2).ToList());
            return list;
        }        
               
        public Calendar OpenArrivalCalendar(bool destination) //bool destination depricated now
        {
            if (!Driver.FindElements(_byArrivalCalendar).Any(el => el.Displayed))  //After filling destination field, arrivalCalendar is autoOpened
            {
                WrappedElement.FindElements(_byDateFields).First().Click();
                Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_byArrivalCalendar));
            }
            return new Calendar(Driver.FindElement(_byArrivalCalendar));
        }               

        public Calendar OpenDepartureCalendar()
        {
            if (!Driver.FindElements(_byDepartureCalendar).Any(el => el.Displayed)) //In new searchForm departureCalendar is autoOpened
            {
                WrappedElement.FindElements(_byDateFields)[2].Click();
                Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(_byDepartureCalendar));
            }
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
            DestinationField.SendKeys(searchData.EnterLocation);

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

        public void DroopedListHandling(string selectLocation)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(_byDroppedList));
            _droppedList.SelectSomeItem(selectLocation);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_byDroppedList));
        }
    }
}
