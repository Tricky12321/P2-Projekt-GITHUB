using System;
public class TestObject : NetworkObject
{
    public int TestID1;
    public string TestID2;
    public string TestID3;
    public string TestID4;
    public string TestID5;
    public string TestID6;

    public void Start()
    {
        //Log.LogData("TestObject", "TestObject Rechived");
        Console.WriteLine("Test object rechived!");
        Console.WriteLine($"{TestID1}");
        Console.WriteLine($"{TestID2}");
        Console.WriteLine($"{TestID3}");
        Console.WriteLine($"{TestID4}");
        Console.WriteLine($"{TestID5}");
        Console.WriteLine($"{TestID6}");
    }

    public TestObject ()
    {

    }

    public TestObject (int Test1, string Test2, string Test3, string Test4, string Test5, string Test6)
    {
        TestID1 = Test1;
        TestID2 = Test2;
        TestID3 = Test3;
        TestID4 = Test4;
        TestID5 = Test5;
        TestID6 = Test6;
    }
    public string[] GetCollumsDB()
    {
        throw new NotImplementedException();
    }

    public int GetID()
    {
        throw new NotImplementedException();
    }

    public string GetIDCollumName()
    {
        throw new NotImplementedException();
    }

    public string GetTableName()
    {
        throw new NotImplementedException();
    }

    public string[] GetValues()
    {
        throw new NotImplementedException();
    }


}

