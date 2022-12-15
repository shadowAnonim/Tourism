using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism
{
    public partial class Tour
    {
        public int Days { get { return (Departure_date - Arrival_date).Days; } }

        public List<Tour_booking> periodBooking { get; set; }
        public List<TourSell> periodSell { get; set; }

        public void SetPeriod(DateTime start, DateTime end)
        {
            periodBooking = Tour_booking.Where(t => t.Booking.Date >= start
                        && t.Booking.Date <= end).ToList();
            periodSell = TourSell.Where(t => t.Sell.Date >= start
                    && t.Sell.Date <= end).ToList();
        }
    }
}
