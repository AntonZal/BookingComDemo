using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyBookingTests.UI.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Pages
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }
        
        private SortBar _sortBar => new SortBar(SortBarElem);
        private FilterPanel _filterPanel => new FilterPanel(FilterPanelElem);

        public string CurrencyType => _header.ActualCurrency.Trim();       

        private By _byCards = By.CssSelector("div[id*='hotellist'] div[data-hotelid]");
        private By _overlay = By.CssSelector(".sr-usp-overlay");
        private List<IWebElement> _cardElements => Driver.FindElements(_byCards).ToList();
        private List<ResultCard> _cards => _cardElements.Select(el => new ResultCard(el)).ToList();

        public List<decimal> PostcardPrices => _cards.Select(card => card.GetPrice()).Where(el => el != default(decimal)).ToList();
        public List<string> PostcardPricesStrings => _cards.Select(card => card.GetPriceString()).Where(el => !string.IsNullOrWhiteSpace(el)).ToList();
        public List<decimal> PostcardRating => _cards.Select(card => card.GetRating()).Where(el => el != default(decimal)).ToList();
        //public List<string> OfferedPrices => PostcardPrices.Select(price => price.Where(char.IsDigit).ToString()).ToList();   //Select(price => Regex.Matches(price, "\\d+")).Cast<>;

        public SearchResultsPage ChangeCurrency(string currencyISO)
        {   
            if (!CurrencyType.Equals(currencyISO))
            {
                _header.OpenCurrencyMenu().SelectCurrency(currencyISO);
            }
            return this;
        }

        public void SortCardsByRating()
        {
            _sortBar.SortByRating();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_overlay));            
        }

        public void SortCardsByPrice()
        {
            _sortBar.SortByPrice();
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_overlay));
        }

        public void FilterCardsByPrice(int min, int max)
        {
            _filterPanel.FilterByPrice(min, max);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_overlay));
        }

        public void FilterCardsByRating(double minRating)
        {
            _filterPanel.FilterByRating(minRating);
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(_overlay));
        }

    }
}
