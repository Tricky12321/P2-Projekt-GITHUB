using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus
{
    public List<StoppestedMTid> busPassagerDataListe = new List<StoppestedMTid>();

    public int busID;
    public string busName;
    public GPS busLok;
    public int passengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute rute;
    public int besøgteStop = 0;

    public override string ToString()
    {
        return busName;
    }
}
