using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.UI.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MyBookingTests.Steps
{
    [Binding]
    public class ResultPageSteps : BaseSteps
    {
        private SearchResultsPage _searchPage = new SearchResultsPage(Driver);
              

        [Given(@"I am on booking result page And I choose english language")]
        public void GivenIAmOnBookingResultPageAndIChooseEnglishLanguage()
        {
            _searchPage = new IndexPage(Driver).Navigate().ChangeLanguage("English (UK)").SendSearchForm(true, true, 1);            
        }

        [When(@"I change currency on result page to '(.*)'")]
        public void WhenIChangeCurrencyOnResultPageTo(string currencyISO)
        {
            _searchPage = _searchPage.ChangeCurrency(currencyISO);
        }

        [Then(@"I will see currency changed to '(.*)' on head element on resultPage")]
        public void ThenIWillSeeCurrencyChangedToOnHeadElementOnResultPage(string currencyISO)
        {
            Assert.That(currencyISO, Is.EqualTo(_searchPage.CurrencyType));            
        }

        [Then(@"I will see currency changed to '(.*)' on postcards prices")]
        public void ThenIWillSeeCurrencyChangedToOnPostcardsPrices(string currencyISO)
        {
             Assert.IsTrue(_searchPage.PostcardPricesStrings.All(c => c.Contains(currencyISO)));
        }

        [When(@"I click sort by rating button")]
        public void WhenIClickSortByRatingButton()
        {
            _searchPage.SortCardsByRating();
        }

        [Then(@"I will see results sorted by rating")]
        public void ThenIWillSeeResultsSortedByRating()
        {
            Assert.That(_searchPage.PostcardRating, Is.Ordered.Descending);
        }

        [When(@"I click sort by price button")]
        public void WhenIClickSortByPriceButton()
        {
            _searchPage.SortCardsByPrice();
        }

        [Then(@"I will see results sorted by price")]
        public void ThenIWillSeeResultsSortedByPrice()
        {
            Assert.That(_searchPage.PostcardPrices, Is.Ordered);
        }

        [When(@"I select '(.*)' And I select duration in (.*) day")]
        public void WhenISelectAndISelectDurationInDay(string currencyISO, int days = 1)
        {
            _searchPage =(new IndexPage(Driver)).ChangeCurrency("currencyISO").SendSearchForm(true, true, days);
        }        

        [When(@"I select filter by price '(.*)', '(.*)'")]
        public void WhenISelectFilterByPrice(int minPrice, int maxPrice)
        {
            _searchPage.FilterCardsByPrice(minPrice, maxPrice);
        }
        
        [Then(@"I will see results filtered by price '(.*)', '(.*)'")]
        public void ThenIWillSeeResultsFilteredByPrice(int minPrice, int maxPrice)
        {                                                      
            //1.02 и 0.98 - коэффициенты, учитывающие граничные округления при переводе валют
            Assert.That(_searchPage.PostcardPrices, Has.All.LessThanOrEqualTo(maxPrice*1.02) & Has.All.GreaterThanOrEqualTo(minPrice*0.98)); 
        }
    }
}
