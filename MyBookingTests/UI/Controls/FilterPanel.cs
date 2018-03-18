using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyBookingTests.UI.Controls
{
    public class FilterPanel : Control
    {
        public FilterPanel(IWebElement element) : base(element)
        {
        }

        protected IReadOnlyCollection<IWebElement> PriceFilters => WrappedElement.FindElements(By.CssSelector("#filter_price .filter_label"));
        protected IReadOnlyCollection<IWebElement> RatingFilters => WrappedElement.FindElements(By.CssSelector("#filter_review .filter_label"));


        public void FilterByPrice(int min, int max)
        {
            var filterToSelect = PriceFilters.FirstOrDefault(el => el.Text.Contains($" {min} ") && el.Text.Contains($" {max} "));
            filterToSelect.Click();
        }

        public void FilterByRating(double minRating)
        {
            var filterToSelect = RatingFilters.FirstOrDefault(el => el.Text.Contains(minRating.ToString()));
            filterToSelect.Click();
        }
    }
}
