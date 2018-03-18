using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyBookingTests.Entities;
using MyBookingTests.UI.Controls;
using MyBookingTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Pages
{
    public class IndexPage : BasePage
    {
        public IndexPage(IWebDriver driver) : base(driver)
        {
        }
               

        private By _byPostcardsPrices = By.XPath(".//div//span[contains(@class, 'price-value')]");
        private By _byCaruselPrices = By.XPath(".//div//div[contains(@class, 'price-overlay')]");

        protected SearchForm _searchForm => new SearchForm(SearchForm);
        public List<IWebElement> DestinationErrors => _searchForm.DestinationErrors();
        public IWebElement DurationError => _searchForm.DurationError;

        public List<string> PostcardPrices => Driver.FindElements(_byPostcardsPrices).ToList().Select(p => p.GetAttribute("innerHTML")).ToList();
        public List<string> CaruselPrices => Driver.FindElements(_byPostcardsPrices).ToList().Select(p => p.GetAttribute("innerHTML")).ToList();

        public string CurrencyType => _header.ActualCurrency.Trim();

        public IndexPage Navigate()
        {
            Driver.Url = ConfigHelper.BaseUrl;
            return this;
        }

        public IndexPage Login()
        {            
            _header.OpenLoginForm().FillLoginForm().Submit();
            return this;
        }

        public void Logout()
        {
            _header.OpenProfileMenu().Exit();
        }

        public IndexPage ChangeLanguage(string lang)
        {
            _header.OpenLanguageMenu().SelectLanguage(lang);
            return this;
        }

        public IndexPage ChangeCurrency(string currencyISO)
        {   //   "if" needed for logged case:
            if (!CurrencyType.Equals(currencyISO))
            {
               _header.OpenCurrencyMenu().SelectCurrency(currencyISO);
            }
            return this;
        }

        public string FlagBelongsTo()
        {
            return _header.GetFlagType();
        }

       public SearchResultsPage SendSearchForm(bool destination = true, bool nearestWeekend = false, int duration = 2, bool remainingFieldsByDefault = true)
        {
            _searchForm.SendSearchForm(destination, nearestWeekend, duration, remainingFieldsByDefault).Submit();
            
            return new SearchResultsPage(Driver);
        }

    }
}
