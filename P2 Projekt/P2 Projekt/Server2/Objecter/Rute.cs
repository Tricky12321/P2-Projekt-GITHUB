using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : MysqlObject
{
    public List<Stoppested> AfPåRuteList = new List<Stoppested>();
    public string RuteName;
    public int RuteID;

    public Rute(string ruteName, int ruteID, params Stoppested[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (Stoppested stop in stoppested)
        {
            AfPåRuteList.Add(stop);
        }
    }

    public Rute() { }

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
        RuteID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        RuteName = TableContent.RowData[0].Values[1];                                           // VARHCAR 50 
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(RuteID.ToString());
        Output.Add(RuteName.ToString());

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
        throw new NotImplementedException();
    }

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }
}
