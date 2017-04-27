using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Test : MysqlObject
{
    public string FirstString = "Hej Med Dig";
    public string SecondString = "Hvad hedder Du";
    public int Age = 21;
    public double Height = 1.80;

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override string[] GetValues()
    {
        throw new NotImplementedException();
    }

    public override string[] GetValuesDB()
    {
        throw new NotImplementedException();
    }

    public override int GetID()
    {
        throw new NotImplementedException();
    }

    public override string GetIDCollumName()
    {
        throw new NotImplementedException();
    }

    public override string GetTableName()
    {
        throw new NotImplementedException();
    }

    public override void GetUpdate()
    {
        throw new NotImplementedException();
    }

    public override void Update(TableDecode TableContent)
    {
        throw new NotImplementedException();
    }

    public override TableDecode GetThisFromDB()
    {
        throw new NotImplementedException();
    }

    public override TableDecode GetThisFromDB(string WhereCondition)
    {
        throw new NotImplementedException();
    }

    public override string WhereID()
    {
        throw new NotImplementedException();
    }
}