using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    class Bus
    {
        public int busID;
        public GPS placering;
        public int passengersTotal;
        public int capacity;
        public Rute rute;

        public Bus (int busID, int capacity, Rute rute)
        {
            this.busID = busID;
            this.capacity = capacity;
            this.rute = rute;
        }

    }
}
