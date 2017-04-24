using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stoppested : MysqlObject
{
    /*public List<StoppestedDataAfPåstigning> AfPåstigningerList = new List<StoppestedDataAfPåstigning>();*/

    //int antalBesøg;
    public int ID;
    public string stoppestedID;
    public GPS stoppestedLok;

    public override string ToString()
    {
        return stoppestedID;
    }
    public override int GetID()
    {
        return this.ID;
    }

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "Stoppesteder";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
        // MysqlControls.UpdateWhere(this.GetTableName(), this.GetCollumsDB(), this.GetValues(), this.WhereID());
    }

    public override void Update(TableDecode TableContent)
    {
        ID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        stoppestedID = TableContent.RowData[0].Values[1];                                   // VARHCAR 50 
        stoppestedLok = new GPS();                                                          
        stoppestedLok.xCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[2]);    // DOUBLE
        stoppestedLok.yCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[3]);    // DOUBLE
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(ID.ToString());
        Output.Add(stoppestedID.ToString());
        Output.Add(stoppestedLok.xCoordinate.ToString());
        Output.Add(stoppestedLok.yCoordinate.ToString());

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

