using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTilBusselskab
{
    public class Stoppested : NetworkObject
    {
        public string StoppestedName;
        public int StoppestedID;
        public GPS StoppestedLok;

        public Stoppested(string name, int ID, GPS coor)
        {
            StoppestedID = ID;
            StoppestedName = name;
            StoppestedLok = coor;
        }

        public Stoppested()
        {

        }
        
        public override string ToString()
        {
            return StoppestedName;
        }

        public void Start()
        {

        }
    }
}
