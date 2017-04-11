using System;
public class TestObject : NetworkObject
{
    int TestID1 = 123;
    string TestID2 = "ASDF";
    string TestID3 = "Hello World!";
    string TestID4 = "!#€%&/()=;:_,.-<>|{}@£$€[]}@";

    public void Start()
    {
        Log.LogData("TestObject", "TestObject Rechived");
        Console.WriteLine("Test object rechived!");
        Console.WriteLine($"{TestID1}");
        Console.WriteLine($"{TestID2}");
        Console.WriteLine($"{TestID3}");
        Console.WriteLine($"{TestID4}");
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

    public void GetUpdate(TableDecode TableContent)
    {
        throw new NotImplementedException();
    }

    public string[] GetValues()
    {
        throw new NotImplementedException();
    }


}

