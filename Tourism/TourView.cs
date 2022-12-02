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
    }
}
