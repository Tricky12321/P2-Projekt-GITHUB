using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : NetworkObject
{
    public string RuteName;
    public int RuteID;
    public List<Stoppested> StoppeSteder = new List<Stoppested>();
    public Rute()
    {

    }

    public Rute(string ruteName, int ruteID, params StoppestedMTid[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (StoppestedMTid stop in stoppested)
        {
            //AfPåRuteListMTid.Add(stop);
        }
    }

    public Rute(string ruteName, int ruteID, params Stoppested[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (Stoppested stop in stoppested)
        {
            StoppeSteder.Add(stop);
        }
    }

    public override string ToString()
    {
        return RuteName;
    }

    public void Start()
    {
        
    }
}
