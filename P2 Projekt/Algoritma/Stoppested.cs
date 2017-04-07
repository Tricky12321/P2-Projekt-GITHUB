using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    class Stoppested
    {
        public List<StoppestedDataAfPåstigning> AfPåstigningerList = new List<StoppestedDataAfPåstigning>();

        int antalBesøg = 0;

        public Stoppested (int ID, coordinate coor, params DateTime[] tidspunkter)
        {
            stoppestedID = ID;

            coordinate lokalitet;
            lokalitet.xCoordinate = coor.xCoordinate;
            lokalitet.yCoordinate = coor.yCoordinate;
            stoppestedLok = lokalitet;

            foreach (DateTime tid in tidspunkter)
            {
                StoppestedDataAfPåstigning placeholder = new StoppestedDataAfPåstigning();
                placeholder.tid = tid;
                AfPåstigningerList.Add(placeholder);
            }
        }

        public void UpdatePassengers (int af, int på)
        {
            AfPåstigningerList[antalBesøg].afstigninger = af;
            AfPåstigningerList[antalBesøg].påstigninger = på;
            ++antalBesøg;
        }

        public int stoppestedID { get; set; }
        public coordinate stoppestedLok { get; set; }

    }
}
