using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MyBookingTests.UI.Controls
{
    public class ProfileMenu : Control
    {
        public ProfileMenu(IWebElement element) : base(element)
        {
        }

        private IWebElement SignOut => WrappedElement.FindElement(By.CssSelector("form[class*=signout] input[type=submit]"));

        public void Exit()
        {
            SignOut.Click();
        }
    }
}
