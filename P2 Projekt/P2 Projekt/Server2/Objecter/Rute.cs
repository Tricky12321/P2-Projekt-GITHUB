using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : MysqlObject
{
    public string RuteName;
    public int RuteID;
    public List<Stoppested> StoppeSteder = new List<Stoppested>();

    public Rute() { }

    public Rute(string ruteName, int ruteID, params Stoppested[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (Stoppested stop in stoppested)
        {
            StoppeSteder.Add(stop);
        }
    }

    public override string ToString()
    {
        return RuteName;
    }

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override int GetID()
    {
        return this.RuteID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "Ruter";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
    }

    public override void Update(TableDecode TableContent)
    {
        Update(TableContent.RowData[0]);

    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(RuteID.ToString());
        Output.Add(RuteName.ToString());
        StringBuilder StoppeStederID = new StringBuilder();
        foreach (var stop in StoppeSteder)
        {
            StoppeStederID.Append(stop.StoppestedID + ",");
        }
        Output.Add(StoppeStederID.ToString().Substring(0, StoppeStederID.Length - 1));

        return Output.ToArray();
    }

    public void Update(Row Row)
    {
        RuteID = Convert.ToInt32(Row.Values[0]);                            // INT 32 ID
        RuteName = Row.Values[1];                                           // VARHCAR 50 
        string[] stoppesteder = Row.Values[2].Split(',');                   // 1,2,3,4,5,6,7,8,9,10
        // stoppesteds id'er:
        // 1,2,3,4,5,6,7,8,9,10
        foreach (string stop in stoppesteder)
        {
            StoppeSteder.Add(new Stoppested(Convert.ToInt32(stop)));
        }
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
        return MysqlControls.SelectAllWhere(GetTableName(), WhereCondition);

    }

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }
}
