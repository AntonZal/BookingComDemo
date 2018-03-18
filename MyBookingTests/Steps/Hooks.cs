using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace MyBookingTests.Steps
{
    [Binding]
    public class Hooks : BaseSteps
    {
        [BeforeScenario]
        public void BeforeFeature()
        {
            Driver = new ChromeDriver();
        }

        [AfterScenario]
        public void AfterFeature()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}
