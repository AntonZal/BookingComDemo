using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class Control
    {
        protected IWebDriver Driver;
        protected IWebElement WrappedElement;
        protected WebDriverWait Wait;
        
        public Control(IWebElement element)
        {
            WrappedElement = element;
            Driver = ((RemoteWebElement)element).WrappedDriver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }
                
    }
}
