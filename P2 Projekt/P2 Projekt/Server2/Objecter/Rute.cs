using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : MysqlObject
{
    public List<Stoppested> AfPåRuteList = new List<Stoppested>();
    /*
    public Rute(string ruteID, params Stoppested[] stoppested)
    {
        this.ruteID = ruteID;
        foreach (Stoppested stop in stoppested)
        {
            AfPåRuteList.Add(stop);
        }
    }
    */
    public string ruteID { get; set; }

    public override string ToString()
    {
        return ruteID;
    }
}
