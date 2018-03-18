using MyBookingTests.UI.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookingTests.Tests
{
    public class ResultsPageTests : BaseTests
    {
        private IndexPage _indexPage;
        private SearchResultsPage _searchResultPage;

        [SetUp]
        public void BeforeEach()
        {
            IndexPage indexPage = new IndexPage(Driver);
            _indexPage = indexPage.Navigate();
        }

        [Test]
        public void ChangeCurrencyResultPage()
        {
            //English Demo version
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").SendSearchForm(true, true);

            string currencyISO = "CAD";
            _searchResultPage = _searchResultPage.ChangeCurrency(currencyISO);
            Assert.That(currencyISO, Is.EqualTo(_searchResultPage.CurrencyType));
            Assert.IsTrue(_searchResultPage.PostcardPricesStrings.All(c => c.Contains(currencyISO)));
        }

        //два следующих теста могут падать, тк не всегда появляется сортировочный бар BasePage.SortBarElem
        //booking.com использует нестрогую сортировк по рейтингу, тест чаще всего падает

        [Test] 
        public void SortByRating()
        {
            //English Demo version for resultPage
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").SendSearchForm(true, true);

            _searchResultPage.SortCardsByRating();
            Assert.That(_searchResultPage.PostcardRating, Is.Ordered.Descending);
        }

        [Test] 
        public void SortByPrice()
        {
            //English Demo version for resultPage
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").SendSearchForm(true, true);

            _searchResultPage.SortCardsByPrice();
            Assert.That(_searchResultPage.PostcardPrices, Is.Ordered);
        }

        [Test]
        public void FilterByPrice()
        {
            //English Demo version for resultPage
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").ChangeCurrency("CAD").SendSearchForm(true, true, 1);
            
            _searchResultPage.FilterCardsByPrice(0, 80);
            Assert.That(_searchResultPage.PostcardPrices, Has.All.LessThanOrEqualTo(80));
        }
                
        [Test]
        public void FilterByRating()
        {
            //English Demo version for resultPage
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").SendSearchForm(true, true);

            _searchResultPage.FilterCardsByRating(8);
            Assert.That(_searchResultPage.PostcardRating, Has.All.GreaterThanOrEqualTo(8));
        }
    }
}
