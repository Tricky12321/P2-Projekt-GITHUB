using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute
{
    public string RuteName;
    public int RuteID;
    public List<StoppestedMTid> AfPåRuteListMTid = new List<StoppestedMTid>();

    public override string ToString()
    {
        return RuteName;
    }

}
