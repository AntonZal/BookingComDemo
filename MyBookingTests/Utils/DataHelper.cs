using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookingTests.Entities;

namespace MyBookingTests.Utils
{
    public class DataHelper
    {
        private static DateTime _arrivalDate;

        public static SearchFormData GenerateSearchData(bool destination, bool nearestWeekend, int duration, bool remainingFieldsByDefault)
        {
            var searchData = new SearchFormData
            {
                Locality = destination ? "Минск" : "",
                ArrivalDate = SetArrivalData(nearestWeekend),
                DepartureDate = SetDeparturedate(duration),
            };

            return searchData;
        }

        private static DateTime SetArrivalData(bool nearestWeekend)
        {            
            if (nearestWeekend)
            {
                _arrivalDate = DateTime.Now.AddDays(1);
                while (_arrivalDate.DayOfWeek != DayOfWeek.Saturday)
                    _arrivalDate = _arrivalDate.AddDays(1);
            }
            else
            {
                _arrivalDate = DateTime.Now.AddDays(10);
            }

            return _arrivalDate;
        }

        private static DateTime SetDeparturedate(int duration)
        {
            return _arrivalDate.AddDays(duration);
        }
    }
}
