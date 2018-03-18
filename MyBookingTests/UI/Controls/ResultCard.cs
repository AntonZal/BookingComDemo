using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyBookingTests.UI.Controls
{
    public class ResultCard : Control
    {
        public ResultCard(IWebElement element) : base(element)
        {
        }

        // можно добавить селектор для цены без скидки: span[class*=toggle_price],
        // но фильтр по цене для разных валют проходит по разным сценариям
        protected IWebElement Price => WrappedElement.FindElement(By.CssSelector("strong>b")); 
        protected IWebElement Rating => WrappedElement.FindElement(By.CssSelector("span[class*=widget__body]~span[class=review-score-badge]"));

        public string GetPriceString()
        {
            try
            {
                return Price.GetAttribute("innerText");
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        public decimal GetPrice() 
        {
            if (string.IsNullOrWhiteSpace(GetPriceString()))
            {
                return default(decimal);
            }

            var regex = new Regex("\\d+");
            var result = regex.Match(Price.GetAttribute("innerText")).Groups[0].ToString();
            return decimal.Parse(result);
        }

        public decimal GetRating()
        {
            try
            {
                var v = Rating.GetAttribute("innerText").Trim();
                return decimal.Parse(v, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            }            
            catch (NoSuchElementException e)
            {
                Console.WriteLine(e);
                return default(decimal);
            }
        }
    }
}
