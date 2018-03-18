using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MyBookingTests.Utils
{
    public class Screenshotter
    {

        protected IWebDriver Driver;

        public Screenshotter(IWebDriver driver)
        {
            Driver = driver;
        }


        public void Snap()
        {
            try
            {
                var autoLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                var saveLocation = Path.Combine(autoLocation.Replace("bin\\Debug", ""), "Resourses\\Screenshots"); 
                CreateDirectory(saveLocation);
                var filename = Path.Combine(saveLocation, GenerateFilename());
                ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filename, ScreenshotImageFormat.Png);
            }
            catch (Exception ex)
            {
                // TODO: add a logger
                Console.WriteLine($"Caught exception while saving a screenshot: {ex.Message}");
            }
        }

        public void CreateDirectory(string saveLocation)
        {
            try
            {
                Directory.CreateDirectory(saveLocation);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught while creating directory for screenshots: {ex.Message}");
            }
        }

        public string GenerateFilename()
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");              
            var filename = TestContext.CurrentContext.Test.Name.Replace(",", "_").Replace("\"", "");            
            return filename + timeStamp + ".png";
        }

    }
}
