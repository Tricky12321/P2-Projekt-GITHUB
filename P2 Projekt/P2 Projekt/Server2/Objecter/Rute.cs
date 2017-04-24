using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : MysqlObject
{
    public List<Stoppested> AfPåRuteList = new List<Stoppested>();

    public int ruteID;
    public string ruteName { get; set; }

    public override string ToString()
    {
        return ruteName;
    }

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override int GetID()
    {
        return this.ruteID;
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
        ruteID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        ruteName = TableContent.RowData[0].Values[1];                                           // VARHCAR 50 
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(ruteID.ToString());
        Output.Add(ruteName.ToString());

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

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }
}
