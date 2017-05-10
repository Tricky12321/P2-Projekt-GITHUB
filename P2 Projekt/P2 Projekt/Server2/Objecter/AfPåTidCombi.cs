using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AfPåTidCombi : MysqlObject
{
    public Tidspunkt Tidspunkt;

    public int ID;
    public int afstigninger;
    public int påstigninger;
    public DateTime datetime;
    public enum day { mandag = 1, tirsdag, onsdag, torsdag, fredag, lørdag, søndag}
    public Stoppested stop;
    public Bus bus;

    public AfPåTidCombi(Tidspunkt tidspunkt)
    {
        Tidspunkt = tidspunkt;
    }

    public AfPåTidCombi(int afstig, int påstig, Stoppested stoppested, Bus bussen, string dayOfWeek)
    {
        afstigninger = afstig;
        påstigninger = påstig;
        stop = stoppested;
        bus = bussen;
        
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
        afstigninger = Convert.ToInt32(row.Values[1]);                  
        påstigninger = Convert.ToInt32(row.Values[2]);
        datetime = DateTime.Now;
        datetime = Convert.ToDateTime(row.Values[3]);
        stop = new Stoppested();
        stop.StoppestedID = Convert.ToInt32(row.Values[4]);
        stop.GetUpdate();
        bus = new Bus();
        bus.BusID = Convert.ToInt32(row.Values[5]);
        bus.GetUpdate();

        /*
        Tidspunkt = new Tidspunkt();
        Tidspunkt.hour = Convert.ToInt32(TableContent.RowData[0].Values[3]);
        Tidspunkt.minute = Convert.ToInt32(TableContent.RowData[0].Values[4])*/
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(ID.ToString());
        Output.Add(afstigninger.ToString());
        Output.Add(påstigninger.ToString());
        Output.Add(datetime.ToString());
        Output.Add(stop.ToString());
        Output.Add(bus.ToString());

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
