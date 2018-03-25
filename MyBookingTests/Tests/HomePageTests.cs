using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyBookingTests.UI.Pages;
using System.Linq;

namespace MyBookingTests.Tests
{
   
    public class HomePageTests : BaseTests
    {
        private IndexPage _indexPage;        

        [SetUp]
        public void BeforeEach()
        {
            IndexPage indexPage = new IndexPage(Driver);
            _indexPage = indexPage.Navigate().Login();
        }

        [TearDown]
        public void AfterEach()
        {
            _indexPage.Logout();
        }

        [Test]
        public void ChangeLaguageIndexPage()
        {
            string lang = "Italiano";
            _indexPage = _indexPage.ChangeLanguage(lang);
            Assert.That(lang, Is.EqualTo(_indexPage.FlagBelongsTo()));
        }

        [Test]
        public void ChangeCurrencyIndexPage()
        {
            string currencyISO = "CAD";
            _indexPage = _indexPage.ChangeCurrency(currencyISO);
            Assert.That(currencyISO, Is.EqualTo(_indexPage.CurrencyType));
            Assert.IsTrue(_indexPage.PostcardPrices.All(c => c.Contains(currencyISO)));
            Assert.IsTrue(_indexPage.CaruselPrices.All(c => c.Contains(currencyISO)));
        }

        [Test]
        public void DestinaitionNotFilled()
        {
            //English Demo version
            _indexPage = _indexPage.ChangeLanguage("English (UK)");

            _indexPage.SendSearchForm(false, true);
            Assert.IsTrue(_indexPage.DestinationErrors.Any(er => er.Displayed));
        }
        
        [Test]
        public void DurationOver30Days()
        {
            //English Demo version
            _indexPage = _indexPage.ChangeLanguage("English (UK)");

            _indexPage.SendSearchForm(true, false, 35);
            Assert.IsTrue(_indexPage.DurationError.Displayed);
        }
        

    }
}
