using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus : NetworkObject
{
    public List<StoppestedMTid> busPassagerDataListe = new List<StoppestedMTid>();

    public string busName;
    public int BusID;
    public GPS placering;
    int _passengersTotal;
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
            busPassagerDataListe.Add(combi);
        }
    }

    public void Start()
    {

    }

    public Bus()
    {

    }

    public int PassengersTotal
    {
        get
        {
            return _passengersTotal;
        }
        set
        {
            if (_passengersTotal + value < 0)
            {
                throw new BusPassengersTotalUnderZeroException("Der kan ikke være færre end nul passagerer i bussen");
            }
            else
            {
                _passengersTotal = value;
            }
        }
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
