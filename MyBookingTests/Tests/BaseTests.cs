using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.Utils;
using System.Threading;

namespace MyBookingTests.Tests
{
    public class BaseTests
    {
        protected IWebDriver Driver;

        [OneTimeSetUp]
        public void Start()
        {
            Driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void Stop()
        {
            Reports report = new Reports();
            Driver.Url = report.CreateReportFile().GetUrlReport();
            Thread.Sleep(5000); // pause to review a report file  located "..\Resourses\Reports\"
            Driver.Quit();
            Driver = null;
        }

        [TearDown]
        public void AfterEach()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                Screenshotter screenshot = new Screenshotter(Driver);
                screenshot.Snap();
            }
        }
    }
}
