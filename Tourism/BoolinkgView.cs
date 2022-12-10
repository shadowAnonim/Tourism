using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism
{
    public partial class Tour_booking
    {
        public decimal Total { get { return Price * People_count; } }
    }

    public partial class TourSell
    {
        public decimal Total { get { return Price * PeopleCount; } }
    }

    public partial class Sell
    {
        public decimal Total
        {
            get { return TourSell.Sum(x => x.Total); }
        }
    }

    public partial class Booking
    {
        public decimal Total
        {
            get { return Tour_booking.Sum(x => x.Total); }
        }
    }
}
