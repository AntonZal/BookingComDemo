using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MyBookingTests.UI.Controls
{
    public class SortBar : Control
    {
        public SortBar(IWebElement element) : base(element)
        {
        }

        private By _byButton = By.CssSelector("button");
        private By _byRating = By.XPath(".//a[contains(@data-category, 'score') and not(contains(@data-category, 'price'))]");
        private By _byPriceBtn = By.XPath(".//a[contains(@data-category, 'price') and not(contains(@data-category, 'score'))]");        

        protected IReadOnlyCollection<IWebElement> SortSelects => WrappedElement.FindElements(By.CssSelector(".sr-sort-dropdown"));

        public void SortByRating()
        {
            if (SortSelects.Any())
            {
                SortSelects.First().FindElements(By.CssSelector("option")).First(o => o.Text.Equals("Review score"));
                return;
            }

            FindSortBy(_byRating).Click();
        }

        public void SortByPrice()
        {
            if (SortSelects.Any())
            {
                SortSelects.First().FindElements(By.CssSelector("option")).First(o => o.Text.Equals("Lowest price first"));
                return;
            }
                
            FindSortBy(_byPriceBtn).Click();
        }

        private IWebElement FindSortBy(By by)
        {
            IWebElement sortBy;
            if (WrappedElement.FindElement(by).Displayed)
            {
                sortBy = WrappedElement.FindElement(by);
            }
            else
            {
                WrappedElement.FindElement(_byButton).Click();
                Wait.Until(ExpectedConditions.ElementIsVisible(by));
                sortBy = WrappedElement.FindElement(by);
            }
            return sortBy;
        }
    }
}
