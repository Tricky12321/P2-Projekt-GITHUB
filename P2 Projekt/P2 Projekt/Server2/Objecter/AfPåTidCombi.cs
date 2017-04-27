using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AfPåTidCombi : MysqlObject
{
    public int ID;
    public Tidspunkt Tidspunkt;
    public int afstigninger;
    public int påstigninger;

    public override string ToString()
    {
        return Tidspunkt.ToString();
    }

    public override void Start()
    {
        throw new NotImplementedException();
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
        ID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        afstigninger = Convert.ToInt32(TableContent.RowData[0].Values[1]);                  // INT 32
        påstigninger = Convert.ToInt32(TableContent.RowData[0].Values[2]);
        Tidspunkt = new Tidspunkt();
        Tidspunkt.hour = Convert.ToInt32(TableContent.RowData[0].Values[3]);
        Tidspunkt.minute = Convert.ToInt32(TableContent.RowData[0].Values[4]);
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(ID.ToString());
        Output.Add(afstigninger.ToString());
        Output.Add(påstigninger.ToString());
        Output.Add(Tidspunkt.hour.ToString());
        Output.Add(Tidspunkt.minute.ToString());

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
