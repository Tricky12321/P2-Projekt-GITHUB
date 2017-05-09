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
    public int PassengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute Rute;

    public EventHandler PassengerUpdate;

    public Bus(string busName, int busID, int capacityStitting, int capacityStanding, Rute rute, params StoppestedMTid[] afPåTidCombi)
    {
        BusID = busID;
        this.busName = busName;
        CapacitySitting = capacityStitting;
        CapacityStanding = capacityStanding;
        Rute = rute;
        foreach (StoppestedMTid combi in afPåTidCombi)
        {
            StoppeStederMTid.Add(combi);
        }
    }

    public void Start()
    {

    }

    public Bus()
    {

    }

    public void TjekInd()
    {
        ++PassengersTotal;
        OnPassengerUpdated();
    }

    public void TjekUd()
    {
        --PassengersTotal;
        OnPassengerUpdated();
    }

    protected virtual void OnPassengerUpdated()
    {
        if (PassengerUpdate != null)
        {
            PassengerUpdate(this, EventArgs.Empty);
        }
    }

    public override string ToString()
    {
        return busName;
    }
}
