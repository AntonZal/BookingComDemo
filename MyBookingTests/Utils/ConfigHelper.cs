using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyBookingTests.Utils
{
    public static class ConfigHelper
    {
        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["baseUrl"];
            }
        }
        
    }
}
