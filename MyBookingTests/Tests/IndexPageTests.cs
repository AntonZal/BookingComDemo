using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyBookingTests.UI.Pages;
using System.Linq;
using MyBookingTests.Utils;
using NUnit.Framework.Interfaces;

namespace MyBookingTests.Tests
{
   
    public class IndexPageTests : BaseTests
    {
        private IndexPage _indexPage;        
        
        [SetUp]
        public void BeforeEach()
        {
            Driver.Manage().Window.Maximize();
            IndexPage indexPage = new IndexPage(Driver);
            _indexPage = indexPage.Navigate();//.Login();
        }

        

        [TestCase("Norsk")]
        [TestCase("Български")]
        [TestCase("Italiano")]
        public void ChangeLaguageIndexPage(string lang)
        {            
            _indexPage = _indexPage.ChangeLanguage(lang);
            Assert.That(lang, Is.EqualTo(_indexPage.FlagBelongsTo()));
        }

        [TestCase("CAD")]
        [TestCase("₪")]
        [TestCase("US$")]
        public void ChangeCurrencyIndexPage(string currencyISO)
        {
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
