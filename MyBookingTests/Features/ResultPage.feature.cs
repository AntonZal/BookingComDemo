﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.0.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MyBookingTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ResultPage")]
    public partial class ResultPageFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ResultPage.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ResultPage", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line 5
 testRunner.Given("I am on booking result page And I choose english language", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Change currency on result page")]
        [NUnit.Framework.TestCaseAttribute("CAD", null)]
        [NUnit.Framework.TestCaseAttribute("₪", null)]
        [NUnit.Framework.TestCaseAttribute("US$", null)]
        public virtual void ChangeCurrencyOnResultPage(string currency, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change currency on result page", exampleTags);
#line 7
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 8
    testRunner.When(string.Format("I change currency on result page to \'{0}\'", currency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
 testRunner.Then(string.Format("I will see currency changed to \'{0}\' on head element on resultPage", currency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 10
 testRunner.Then(string.Format("I will see currency changed to \'{0}\' on postcards prices", currency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sort search results by rating")]
        public virtual void SortSearchResultsByRating()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sort search results by rating", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 21
     testRunner.When("I click sort by rating button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
  testRunner.Then("I will see results sorted by rating", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sort search results by price")]
        public virtual void SortSearchResultsByPrice()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sort search results by price", ((string[])(null)));
#line 24
 this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 25
     testRunner.When("I click sort by price button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
  testRunner.Then("I will see results sorted by price", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Filter search results by price")]
        [NUnit.Framework.TestCaseAttribute("CAD", "0", "79", null)]
        [NUnit.Framework.TestCaseAttribute("₪", "210", "430", null)]
        [NUnit.Framework.TestCaseAttribute("US$", "120", "180", null)]
        public virtual void FilterSearchResultsByPrice(string currency, string minPrice, string maxPrice, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Filter search results by price", exampleTags);
#line 28
this.ScenarioSetup(scenarioInfo);
#line 3
this.FeatureBackground();
#line 31
  testRunner.When(string.Format("I change currency on result page to \'{0}\'", currency), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
     testRunner.When(string.Format("I select filter by price \'{0}\', \'{1}\'", minPrice, maxPrice), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
  testRunner.Then(string.Format("I will see results filtered by price \'{0}\', \'{1}\'", minPrice, maxPrice), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion