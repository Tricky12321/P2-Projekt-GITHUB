using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stoppested : MysqlObject
{
    /*public List<StoppestedDataAfPåstigning> AfPåstigningerList = new List<StoppestedDataAfPåstigning>();*/

    public string StoppestedName;
    public int StoppestedID;
    public GPS StoppestedLok;

    public Stoppested(string name, int ID, GPS coor)
    {
        StoppestedID = ID;
        StoppestedName = name;
        StoppestedLok = coor;
    }

    public Stoppested()
    {

    }

    public override string ToString()
    {
        return StoppestedName;
    }
    public override int GetID()
    {
        return this.StoppestedID;
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
        StoppestedID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        StoppestedName = TableContent.RowData[0].Values[1];                                   // VARHCAR 50 
        StoppestedLok = new GPS();
        StoppestedLok.xCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[2]);    // DOUBLE
        StoppestedLok.yCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[3]);    // DOUBLE
    }

    public void Update(Row Row)
    {
        StoppestedID = Convert.ToInt32(Row.Values[0]);                            // INT 32 ID
        StoppestedName = Row.Values[1];                                   // VARHCAR 50 
        StoppestedLok = new GPS();
        StoppestedLok.xCoordinate = Convert.ToDouble(Row.Values[2]);    // DOUBLE
        StoppestedLok.yCoordinate = Convert.ToDouble(Row.Values[3]);    // DOUBLE
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(StoppestedID.ToString());
        Output.Add(StoppestedName);
        Output.Add(StoppestedLok.xCoordinate.ToString());
        Output.Add(StoppestedLok.yCoordinate.ToString());

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

    public override TableDecode GetThisFromDB(string WhereCondition)
    {
        return MysqlControls.SelectAllWhere(GetTableName(), WhereCondition);

    }
}

