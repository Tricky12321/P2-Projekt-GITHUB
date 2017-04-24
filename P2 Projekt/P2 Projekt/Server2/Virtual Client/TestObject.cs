using System;
using System.Collections.Generic;
public class TestObject : MysqlObject
{
    public int ID;
    public int TestID1;
    public string TestID2;
    public string TestID3;
    public string TestID4;
    public string TestID5;
    public string TestID6;

    public const bool ShowInfo = false;

    public override void Start()
    {
        Log.LogData("TestObject", "TestObject Rechived");
        //Console.WriteLine("Test object rechived!");
        /*
        if (ShowInfo)
        {
            Console.WriteLine($"{TestID1}");
            Console.WriteLine($"{TestID2}");
            Console.WriteLine($"{TestID3}");
            Console.WriteLine($"{TestID4}");
            Console.WriteLine($"{TestID5}");
            Console.WriteLine($"{TestID6}");
        }
        else
        {
            // Console.WriteLine("Data er skjult");
        }
        */
        UploadToDatabase();

    }

    public TestObject ()
    {

    }

    public TestObject (int ID ,int Test1, string Test2, string Test3, string Test4, string Test5, string Test6)
    {
        this.ID = ID;
        TestID1 = Test1;
        TestID2 = Test2;
        TestID3 = Test3;
        TestID4 = Test4;
        TestID5 = Test5;
        TestID6 = Test6;
    }

    public override int GetID()
    {
        return ID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "TestObject";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
        // MysqlControls.UpdateWhere(this.GetTableName(), this.GetCollumsDB(), this.GetValues(), this.WhereID());
    }

    public override void Update(TableDecode TableContent)
    {
        this.ID = Convert.ToInt32(TableContent.RowData[0].Values[0]);
        this.TestID1 = Convert.ToInt32(TableContent.RowData[0].Values[1]);
        this.TestID2 = TableContent.RowData[0].Values[2];
        this.TestID3 = TableContent.RowData[0].Values[3];
        this.TestID4 = TableContent.RowData[0].Values[4];
        this.TestID5 = TableContent.RowData[0].Values[5];
        this.TestID6 = TableContent.RowData[0].Values[6];
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(ID.ToString());
        Output.Add(TestID1.ToString());
        Output.Add(TestID2);
        Output.Add(TestID3);
        Output.Add(TestID4);
        Output.Add(TestID5);
        Output.Add(TestID6);

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

