using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyBookingTests.Utils
{
    public class Reports
    {
        private string autoLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        private string baseDir => Path.Combine(autoLocation.Replace("bin\\Debug", ""), "Resourses");           
        private string screenshotsDir => Path.Combine(baseDir, "Screenshots");
        private string thumbsDir => Path.Combine(screenshotsDir, "thumbs");
        private string reportFile => Path.Combine(baseDir, "Reports", "report.html");

        private IEnumerable<string> imageNamesList => Directory.EnumerateFiles(screenshotsDir, "*.png");

        public Reports CreateReportFile()
        {
            try
            {
                CreateThumbsDir(thumbsDir);
                CreatePreviewImages();
                CreateHtmlFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught while creating a thumbnail dir: {ex.Message}");
            }
            return this;
        }

        public void CreateThumbsDir(string location)
        {
            Directory.CreateDirectory(location);            
        }        

        public void CreatePreviewImages(double percent = 0.3)
        {
            foreach (string imageName in imageNamesList)
            {
                Bitmap image = new Bitmap(Image.FromFile(imageName));
                Size s = image.Size;
                var w = (int)(s.Width * percent);
                var h = (int)(s.Height * percent);
                var newName = thumbsDir + imageName.Replace(screenshotsDir, "");
                Bitmap image1 = new Bitmap(Image.FromFile(imageName), w, h);
                image1.Save(newName);
            }
        }

        public void CreateHtmlFile()
        {
            string textReport = CreateHeaderText() + CreateBodyBegining() + CreateBodyText() + CreateBodyEnd();

            if (!File.Exists(reportFile))
            {
                File.Create(reportFile);
            }

            StreamWriter output = new StreamWriter(reportFile);

            output.WriteLine(textReport);
            output.Close();
        }

        private string CreateHeaderText()
        {
            return File.ReadAllText(Path.Combine(baseDir, "ReportResourses", "header.txt"));
        }

        public string CreateBodyBegining()
        {
            return "<body><div class=\"content\"><section id=\"examples\"class=\"examples-section\"><div class=\"container\"><h1>Failed Tests:</h1>\r\n";        
        }

        public string CreateBodyEnd()
        {
            return "</div></section></div></body></html>";
        }

        public string CreateBodyText()
        {
            string text = "";           

            foreach (string image in imageNamesList)
            {
                var testName = image.Replace(screenshotsDir, "").Replace(".png", "");
                var fileName = image.Replace(" ", "%20");
                var previewFileName = image.Replace(screenshotsDir, thumbsDir).Replace(" ", "%20");               
                var newString = File.ReadAllText(Path.Combine(baseDir, "ReportResourses\\body.txt"));
                newString = string.Format(newString, testName, fileName, previewFileName);
                text += newString;
            }  
            
            return text;
        }
        
        public string GetUrlReport()
        {
            return reportFile;
        }
    }
}
