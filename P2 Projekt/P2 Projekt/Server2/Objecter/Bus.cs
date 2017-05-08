using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus : MysqlObject
{
    public List<StoppestedMTid> busPassagerDataListe = new List<StoppestedMTid>();

    public string busName;
    public int BusID;
    public GPS placering;
    int _passengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute Rute;

    public Bus()
    {

    }

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

    public override void Start()
    {
        this.UploadToDatabase();
    }

    public override int GetID()
    {
        return this.BusID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "Busser";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
    }

    public override void Update(TableDecode TableContent)
    {
        if (TableContent.Count == 0)
        {
            throw new NoObjectFoundException("Der blev ikke fundet noget object i databasen med de kriterier");
        }
        BusID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        placering = new GPS();
        busName = Convert.ToString(TableContent.RowData[0].Values[1]);
        placering.xCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[2]);    // DOUBLE
        placering.yCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[3]);    // DOUBLE
        PassengersTotal = Convert.ToInt32(TableContent.RowData[0].Values[4]);
        CapacitySitting = Convert.ToInt32(TableContent.RowData[0].Values[5]);
        CapacityStanding = Convert.ToInt32(TableContent.RowData[0].Values[6]);
        Rute = new Rute();
        Rute.RuteID = Convert.ToInt32(TableContent.RowData[0].Values[8]);            // Ruten her mangler at være korrekt
        Rute.GetUpdate();
        // rute = Convert.ToInt32(TableContent.RowData[0].Values[7]);                // Se også lige om den er korrekt i GetValues

    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(BusID.ToString());               // 1
        Output.Add(busName.ToString());             // 2
        Output.Add(placering.xCoordinate.ToString());  // 3
        Output.Add(placering.yCoordinate.ToString());  // 4
        Output.Add(PassengersTotal.ToString());     // 5
        Output.Add(CapacityStanding.ToString());    // 6
        Output.Add(CapacitySitting.ToString());     // 7
        //Output.Add(besøgteStop.ToString());         // 8
        Output.Add(Rute.RuteID.ToString());         // 9

        return Output.ToArray();
    }

    public override string[] GetValuesDB()
    {
        return GetThisFromDB().RowData[0].Values.ToArray();
    }

    public override TableDecode GetThisFromDB()
    {
        return GetThisFromDB(WhereID());
    }

    public override TableDecode GetThisFromDB(string WhereCondition)
    {
        if (WhereCondition == "None")
        {
            return MysqlControls.SelectAll(GetTableName());
        }
        else
        {
            return MysqlControls.SelectAllWhere(GetTableName(), WhereCondition);
        }
    }

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }

    public EventHandler PassengerUpdate;

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
