using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface NetworkObject
{
    void Start();

    string[] GetCollumsDB();

    string[] GetValues();

    string[] GetValuesDB();

    int GetID();

    string GetIDCollumName();

    string GetTableName();

    void GetUpdate();

    void Update(TableDecode TableContent);

    TableDecode GetThisFromDB();

    string WhereID();
}