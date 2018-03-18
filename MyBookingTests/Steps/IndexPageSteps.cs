using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.Entities;
using MyBookingTests.UI.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MyBookingTests.Steps
{
    [Binding]
    public class IndexPageSteps : BaseSteps
    {
        [Given(@"I am on booking index page")]
        public void GivenIAmOnBookingIndexPage()
        {
            new IndexPage(Driver).Navigate();            
        }

        [When(@"I change language to '(.*)'")]
        public void WhenIChangeLanguageTo(string languageName)
        {
            new IndexPage(Driver).ChangeLanguage(languageName);
        }

        [Then(@"I will see language changed to '(.*)'")]
        public void ThenIWillSeeLanguageChangedTo(string languageName)
        {
            Assert.That(languageName, Is.EqualTo(new IndexPage(Driver).FlagBelongsTo()));
        }

        [When(@"I change currency to '(.*)'")]
        public void WhenIChangeCurrencyTo(string currencyISO)
        {
            new IndexPage(Driver).ChangeCurrency(currencyISO);
        }
                
        [Then(@"I will see currency changed to '(.*)' on head element")]
        public void ThenIWillSeeCurrencyChangedToOnHeadElement(string currencyISO)
        {
            Assert.That(currencyISO, Is.EqualTo(new IndexPage(Driver).CurrencyType));
        }

        [Then(@"I will see currency changed to '(.*)' on postcard prices")]
        public void ThenIWillSeeCurrencyChangedToOnPostcardPrices(string currencyISO)
        {
            Assert.IsTrue(new IndexPage(Driver).PostcardPrices.All(c => c.Contains(currencyISO)));
        }

        [Then(@"I will see currency changed to '(.*)' on caruosel elements")]
        public void ThenIWillSeeCurrencyChangedToOnCaruoselElements(string currencyISO)
        {
            Assert.IsTrue(new IndexPage(Driver).CaruselPrices.All(c => c.Contains(currencyISO)));
        }        

        [When(@"I fill in the Search form")]
        public void WhenIFillInTheSearchForm(Table table)
        {   //English Demo version
            new IndexPage(Driver).ChangeLanguage("English (UK)").FillSearchForm(table);
           
        }

        // The second way to create data inside script
        [When(@"I fill in the Search form 2")]
        public void WhenIFillInTheSearchForm2(Table table)
        {   //English Demo version
            var formData = table.CreateInstance<SearchFormData>();   // table.CreateSet<SearchFormData>();
            new IndexPage(Driver).ChangeLanguage("English (UK)").FillSearchForm(formData);                    
        }

        [Then(@"I will see warning message")]
        public void ThenIWillSeeWarningMessage()
        {
            Assert.IsTrue(new IndexPage(Driver).DestinationErrors.Any(er => er.Displayed)); 
        }

        [Then(@"I will see warning duration message")]
        public void ThenIWillSeeWarningDurationMessage()
        {
            Assert.IsTrue(new IndexPage(Driver).DurationError.Displayed);
        }
    }
}
