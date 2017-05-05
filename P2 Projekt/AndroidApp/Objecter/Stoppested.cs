using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stoppested
{
    /*public List<StoppestedDataAfPåstigning> AfPåstigningerList = new List<StoppestedDataAfPåstigning>();*/

    //int antalBesøg;
    public int ID;
    public string stoppestedID;
    public GPS stoppestedLok;

    public override string ToString()
    {
        return stoppestedID;
    }
}

