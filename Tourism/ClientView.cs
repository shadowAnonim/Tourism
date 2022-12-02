using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism
{
    public partial class Client
    {
        public string FullName { get { return First_name + " " + Last_name; } }
    }
}
