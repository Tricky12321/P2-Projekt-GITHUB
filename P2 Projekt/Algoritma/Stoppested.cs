using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritma
{
    struct coordinate
    {
        int xCoordinate;
        int yCoordinate;
    }

    struct passagerAfPå
    {
        DateTime tid;
        int afstigninger;
        int påstigninger;
    }

    class Stoppested
    {
        List<passagerAfPå> AfPåstigningerList = new List<passagerAfPå>();
        public Stoppested (params DateTime[] besøgstider)
        {
            foreach(DateTime tidspunkt in besøgstider)
            {
                
            }
        }

        public int stoppestedID { get; set; }
        public coordinate stoppestedLok { get; set; }

    }
}
