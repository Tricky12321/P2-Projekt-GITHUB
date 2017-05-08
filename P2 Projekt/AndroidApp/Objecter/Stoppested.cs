using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stoppested : NetworkObject
{
    /*public List<StoppestedDataAfPåstigning> AfPåstigningerList = new List<StoppestedDataAfPåstigning>();*/

    //int antalBesøg;
    public string StoppestedName;
    public int StoppestedID;
    public GPS StoppestedLok;

    public override string ToString()
    {
        return StoppestedName;
    }

    public void Start()
    {

    }
}

