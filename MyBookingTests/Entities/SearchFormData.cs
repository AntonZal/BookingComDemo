using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookingTests.Entities
{
    public class SearchFormData
    {
        public string EnterLocation { get; set; }
        public string SelectLocation { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public int? Rooms { get; set; }
    }
}
