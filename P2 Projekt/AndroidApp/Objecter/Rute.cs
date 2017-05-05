using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute
{
    public List<Stoppested> AfPåRuteList = new List<Stoppested>();

    public int ruteID;
    public string ruteName;

    public override string ToString()
    {
        return ruteName;
    }

}
