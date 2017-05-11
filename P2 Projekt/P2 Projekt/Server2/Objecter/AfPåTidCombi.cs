using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum day { mandag = 1, tirsdag = 2, onsdag = 3, torsdag = 4, fredag = 5, lørdag = 6, søndag = 7 }


public class AfPåTidCombi : MysqlObject
{
    public Tidspunkt Tidspunkt;
    public int ID;
    public int Afstigninger;
    public int Påstigninger;
    public day UgeDag;
    public Stoppested Stop;
    public Bus Bus;
    public int TotalPassagere;
    public int MaxCapa;
    public int Week;
    public AfPåTidCombi(Tidspunkt tidspunkt)
    {
        Tidspunkt = tidspunkt;
    }

    public AfPåTidCombi(int afstig, int påstig, Stoppested stoppested, Bus bussen, day dayOfWeek, int Week, Tidspunkt tidspunkt, int Total, int Max)
    {
        Afstigninger = afstig;
        Påstigninger = påstig;
        Stop = stoppested;
        Bus = bussen;
        Tidspunkt = tidspunkt;
        UgeDag = dayOfWeek;
        TotalPassagere = Total;
        MaxCapa = Max;
        this.Week = Week;

    }

    public AfPåTidCombi() { }

    public override string ToString()
    {
        return Tidspunkt.ToString();
    }

    public override void Start()
    {
        this.UploadToDatabase();
    }

    public override int GetID()
    {
        return this.ID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "AfpaaTid";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
    }

    public override void Update(TableDecode TableContent)
    {
        Update(TableContent.RowData[0]);
    }

    public void Update(Row row)
    {
        ID = Convert.ToInt32(row.Values[0]);                          
        Afstigninger = Convert.ToInt32(row.Values[1]);                  
        Påstigninger = Convert.ToInt32(row.Values[2]);
        TotalPassagere = Convert.ToInt32(row.Values[3]);
        MaxCapa = Convert.ToInt32(row.Values[4]);
        Tidspunkt = new Tidspunkt();
        Tidspunkt.hour = Convert.ToInt32(row.Values[5]);
        Tidspunkt.minute = Convert.ToInt32(row.Values[6]);
        UgeDag = (day)Convert.ToInt32(row.Values[7]);
        Week = Convert.ToInt32(row.Values[8]);
        Stop = new Stoppested();
        Stop.StoppestedID = Convert.ToInt32(row.Values[9]);
        Stop.GetUpdate();
        Bus = new Bus();
        Bus.BusID = Convert.ToInt32(row.Values[10]);
        Bus.GetUpdate();
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        if (ID != 0)
        {
            Output.Add(ID.ToString());

        } else
        {
            Output.Add("NULL");

        }
        Output.Add(Afstigninger.ToString());
        Output.Add(Påstigninger.ToString());
        Output.Add(TotalPassagere.ToString());
        Output.Add(MaxCapa.ToString());
        Output.Add(Tidspunkt.hour.ToString());
        Output.Add(Tidspunkt.minute.ToString());
        Output.Add(Convert.ToInt32(UgeDag).ToString());
        Output.Add(Week.ToString());
        Output.Add(Stop.StoppestedID.ToString());
        Output.Add(Bus.BusID.ToString());

        return Output.ToArray();
    }

    public override string[] GetValuesDB()
    {
        return GetThisFromDB().RowData[0].Values.ToArray();
    }

    public override TableDecode GetThisFromDB()
    {
        return MysqlControls.SelectAllWhere(GetTableName(), WhereID());
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
}
