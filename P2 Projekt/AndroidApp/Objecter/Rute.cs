using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute
{
    public string RuteName;
    public int RuteID;
    public List<Stoppested> StoppeSteder = new List<Stoppested>();

    public override string ToString()
    {
        return RuteName;
    }

}
