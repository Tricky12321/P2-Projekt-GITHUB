using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute
{
    //public List<Stoppested> AfPåRuteList = new List<Stoppested>();
    public List<StoppestedMTid> AfPåRuteListMTid = new List<StoppestedMTid>();
    public string RuteName;
    public int RuteID;
    //public List<StoppestedMTid> AfPåRuteListMTid = new List<StoppestedMTid>();

    public Rute(string ruteName, int ruteID, params StoppestedMTid[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (StoppestedMTid stop in stoppested)
        {
            AfPåRuteListMTid.Add(stop);
        }
    }

    public Rute(string ruteName, int ruteID, params Stoppested[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (Stoppested stop in stoppested)
        {
            AfPåRuteListMTid.Add(new StoppestedMTid(stop));
        }
    }

    public override string ToString()
    {
        return RuteName;
    }
}
