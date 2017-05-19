using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
public class Bus : MysqlObject
{
    

    public string busName;
    public int BusID;
    public GPS placering;
    public int PassengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute Rute;
    public List<StoppestedMTid> StoppeStederMTid = new List<StoppestedMTid>();
    public int TotalCapacity => CapacitySitting + CapacityStanding;
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
            StoppeStederMTid.Add(combi);
        }
    }

    public override void Start()
    {
        this.UploadToDatabase();
    }

    public override int GetID()
    {
        return BusID;
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
        Update(TableContent.RowData[0]);
    }

    public void Update(Row Row)
    {
        Rute = new Rute();
        placering = new GPS();

        BusID = Convert.ToInt32(Row.Values[0]);                             // INT 32 ID
        busName = Convert.ToString(Row.Values[1]);                          // VARCHAR
        placering.xCoordinate = Convert.ToDouble(Row.Values[2]);            // DOUBLE
        placering.yCoordinate = Convert.ToDouble(Row.Values[3]);            // DOUBLE
        PassengersTotal = Convert.ToInt32(Row.Values[4]);                   // INT
        CapacitySitting = Convert.ToInt32(Row.Values[5]);                   // INT
        CapacityStanding = Convert.ToInt32(Row.Values[6]);                  // INT
        Rute.RuteID = Convert.ToInt32(Row.Values[7]);                       // INT
        Rute.GetUpdate();  // Henter ruten fra databasen
        string[] StoppeSteder = Row.Values[8].Replace(".",",").Split(',');
        string[] Afvigelser = Row.Values[9].Replace(".",",").Split(',');
        int i = 0;
        foreach (Stoppested stop in Rute.StoppeSteder)
        {
            List<AfPåTidCombi> AfTidList = new List<AfPåTidCombi>();
            // {11:30;12:30;13:30}
            string times = StoppeSteder[i].Replace("}", "").Replace("{", "");
            string AfvigelseStpå = Afvigelser[i].Replace("}", "").Replace("{", "");
            // 11:30;12:30;13:30
            string[] tider = times.Split(';');
            string[] Afvigelse = AfvigelseStpå.Split(';');
            int k = 0;
            foreach (string singleTid in tider)
            {
                // 11:30
                Regex TidRegex = new Regex("^[0-9]{2}:[0-9]{2}$");
                if (TidRegex.IsMatch(singleTid))
                {
                    AfPåTidCombi AfPåObj = new AfPåTidCombi(new Tidspunkt(singleTid));
                    AfPåObj.ForventetPassagere = Convert.ToInt32(Afvigelse[k]);
                    AfTidList.Add(AfPåObj);
                }
            }
            StoppeStederMTid.Add(new StoppestedMTid(stop, AfTidList));
            i++;
        }
    }

    public override string[] GetValues()
    {
        if (placering == null)
        {
            placering = StoppeStederMTid[0].Stop.StoppestedLok;
        }
        List<string> Output = new List<string>();
        Output.Add(BusID.ToString());                                    // 1
        Output.Add(busName.ToString());                                  // 2
        Output.Add(placering.xCoordinate.ToString().Replace(",","."));   // 3
        Output.Add(placering.yCoordinate.ToString().Replace(",", "."));  // 4
        Output.Add(PassengersTotal.ToString());                          // 5
        Output.Add(CapacitySitting.ToString());                          // 6
        Output.Add(CapacityStanding.ToString());                         // 7
        Output.Add(Rute.RuteID.ToString());                              // 8
        StringBuilder StoppeStederTID = new StringBuilder();
        StringBuilder AfvigelseSTB = new StringBuilder();
        int i = 0;
        foreach (Stoppested stop in Rute.StoppeSteder)
        {
            StoppeStederTID.Append("{");
            AfvigelseSTB.Append("{");
            foreach (AfPåTidCombi stopmtid in StoppeStederMTid[i].AfPåTidComb)
            {
                    StoppeStederTID.Append(stopmtid.Tidspunkt.SinpleString() + ";");
                    AfvigelseSTB.Append(stopmtid.ForventetPassagere.ToString() + ";");
            }
            AfvigelseSTB.Append("}.");
            StoppeStederTID.Append("}.");
            i++;
        }
        string strtoadd = StoppeStederTID.ToString();
        string strtoadd2 = AfvigelseSTB.ToString();
        strtoadd = StoppeStederTID.ToString().Substring(0, StoppeStederTID.Length - 1).Replace(";}", "}");
        strtoadd2 = AfvigelseSTB.ToString().Substring(0, AfvigelseSTB.Length - 1).Replace(";}", "}");
        Output.Add(strtoadd);
        Output.Add(strtoadd2);

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

    public override string ToString()
    {
        return busName;
    }
}
