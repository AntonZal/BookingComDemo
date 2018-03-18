using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Driver.Quit();
            Driver = null;
        }

    }
}
