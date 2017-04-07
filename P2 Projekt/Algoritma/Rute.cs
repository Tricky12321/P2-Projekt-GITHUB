using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    class Rute
    {
        public List<Stoppested> AfPåRuteList = new List<Stoppested>();
        
        public Rute (int ruteID, params Stoppested[] stoppestedID)
        {
            ruteID = this.ruteID;
            foreach (Stoppested stop in stoppestedID)
            {
                AfPåRuteList.Add(stop);
            }
        }

        public int ruteID { get; set; }
    }
}
