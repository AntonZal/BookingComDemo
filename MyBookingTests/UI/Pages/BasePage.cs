using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.UI.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected IWebElement HeaderElem => Driver.FindElement(By.CssSelector("div.header-wrapper"));
        protected IWebElement LoginFormElem => Driver.FindElement(By.CssSelector("body>div[class*=user-access-menu]"));
        protected IWebElement SearchForm => Driver.FindElement(By.CssSelector("h1~div[class*=searchbox__outer]"));
        protected IWebElement SortBarElem => Driver.FindElement(By.CssSelector("div[data-block-id=sort_bar]"));

        protected IWebElement FilterPanelElem => Driver.FindElement(By.CssSelector("#filterbox_wrap"));


        protected Header _header => new Header(HeaderElem);
        

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }
    }
}
