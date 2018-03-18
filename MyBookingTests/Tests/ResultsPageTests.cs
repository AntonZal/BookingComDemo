using MyBookingTests.UI.Pages;
using MyBookingTests.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
            Driver.Manage().Window.Maximize();
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

        //booking.com использует нестрогую сортировку
        //два следующих теста чаще всего падают

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

        [TestCase("CAD", "0", "79")]
        [TestCase("₪", "210", "430")]
        [TestCase("US$", "120", "180")]        
        public void FilterByPrice(string currencyISO, string minPrice, string maxPrice)
        {
            var min = Int32.Parse(minPrice);
            var max = Int32.Parse(maxPrice);
            //English Demo version for resultPage
            _searchResultPage = _indexPage.ChangeLanguage("English (UK)").SendSearchForm(true, true, 1);
            
            _searchResultPage.ChangeCurrency(currencyISO).FilterCardsByPrice(min, max);
            //1.02 и 0.98 - коэффициенты, учитывающие граничные округления при переводе валют
            Assert.That(_searchResultPage.PostcardPrices, Has.All.LessThanOrEqualTo(max * 1.02) & Has.All.GreaterThanOrEqualTo(min * 0.98));
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
