using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MyBookingTests.UI.Controls
{
    public class DroopedList : Control
    {
        public DroopedList(IWebElement element) : base(element)
        {
        }

        protected IReadOnlyCollection<IWebElement> Items => WrappedElement.FindElements(By.CssSelector("li[role]"));

        public void SelectSomeItem()
        {
            Items.FirstOrDefault().Click();
        }

        public void SelectSomeItem(string selectLocation)
        {
            Items.FirstOrDefault(i => i.GetAttribute("innerText").Contains(selectLocation)).Click();
        }
    }
}
