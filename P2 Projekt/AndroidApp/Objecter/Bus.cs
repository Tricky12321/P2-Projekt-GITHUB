using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus : NetworkObject
{
    public List<StoppestedMTid> StoppeStederMTid = new List<StoppestedMTid>();

    public string busName;
    public int BusID;
    public GPS placering;
    int _passengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute Rute;

    public override string ToString()
    {
        return busName;
    }

    public void Start()
    {

    }
}
