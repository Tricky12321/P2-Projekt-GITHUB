using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface NetworkObject
{
    void Start();

    string[] GetCollumsDB();

    int GetID();

    string GetIDCollumName();

    string GetTableName();

    void GetUpdate(TableDecode TableContent);

    string[] GetValues();
}