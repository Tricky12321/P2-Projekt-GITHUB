using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    class Bus
    {
        List<DataAfPåstigning> busPassagerListe = new List<DataAfPåstigning>();

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

            /*
            EventArgs += new TjekIndEventHandler(TjekInd);
            EventArgs += new TjekUdEventHandler(TjekUd);
            */
        }

        public void TjekInd(object sender, EventArgs e)
        {
            ++passengersTotal;
        }

        public void TjekUd()
        {
            --passengersTotal;
        }


    }
}
