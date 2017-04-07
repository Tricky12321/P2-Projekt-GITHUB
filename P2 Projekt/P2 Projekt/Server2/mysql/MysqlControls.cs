using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class MysqlControls
{
    public static bool Insert(string table, string[] colums, string[] values)
    {
        if (colums.Count() != values.Count())
        {
            throw new NotValidQueryException("There must be the same amount of colums as values");
        }
        // --------------------------------------------------
        // Handle Colums
        // --------------------------------------------------
        #region Colums
        string ColumsQuery;
        ColumsQuery = "(";
        foreach (string colum in colums)
        {
            if (colum == colums.Last())
            {
                ColumsQuery += $"`{colum}`";
            }
            else
            {
                ColumsQuery += $"`{colum}`,";
            }
        }
        ColumsQuery += ")";
        #endregion
        // --------------------------------------------------
        // Handle Values
        // --------------------------------------------------
        #region Values

        string ValuesQuery;
        ValuesQuery = "(";
        foreach (string value in values)
        {
            if (value == "NULL")
            {
                ValuesQuery += $"{value}";
            }
            else
            {
                ValuesQuery += $"'{value}'";
            }
            if (value != values.Last())
            {
                ValuesQuery += ",";
            }
        }
        ValuesQuery += ")";
        #endregion
        // --------------------------------------------------
        string TotalQuery = $"INSERT INTO `{table}` {ColumsQuery} VALUES {ValuesQuery};";
        Mysql.RunQuery(TotalQuery);
        return true;

    }

    public static TableDecode Select(string table, string condition)
    {
        return Mysql.RunQueryWithReturn($"SELECT `{condition}` FROM `{table}`");
    }

    public static TableDecode SelectAll(string table)
    {
        return Mysql.RunQueryWithReturn($"SELECT * FROM `{table}`");
    }

    public static TableDecode SelectAllWhere(string table, string whereCondition)
    {
        return Mysql.RunQueryWithReturn($"SELECT * FROM `{table}` WHERE {whereCondition}");
    }

    public static TableDecode SelectWhere(string table, string condition, string whereCondition)
    {
        return Mysql.RunQueryWithReturn($"SELECT `{condition}` FROM `{table}` WHERE {whereCondition}");
    }

    public static void DelteAll(string table)
    {
        Mysql.RunQueryWithReturn($"DELETE * FROM `{table}`");
    }

    public static void DelteAllWhere(string table, string whereCondition)
    {
        Mysql.RunQueryWithReturn($"DELETE * FROM `{table}` WHERE {whereCondition}");
    }

    public static void DelteWhere(string table, string condition, string whereCondition)
    {
        Mysql.RunQueryWithReturn($"DELETE {condition} FROM `{table}` WHERE {whereCondition}");
    }

    public static void UpdateAll(string table, string colums, string values)
    {
        string Query;
        Query = $"{colums}='{values}'";
        Mysql.RunQuery($"UPDATE `{table}` SET {Query}");
    }

    public static void UpdateWhere(string table, string colums, string values, string whereCondition)
    {
        string Values;
        Values = $"{colums}='{values}'";
        Mysql.RunQuery($"UPDATE `{table}` SET {Values} WHERE {whereCondition}");
    }

    public static void UpdateNetworkObject(NetworkObject NWO)
    {
        int count = NWO.GetCollumsDB().Count();
        for (int i = 0; i < count; i++)
        {
            UpdateWhere(NWO.GetTableName(), NWO.GetCollumsDB()[i], NWO.GetValues()[i], $"`{NWO.GetIDCollumName()}`='{NWO.GetID()}'");
        }
    }

    public static TableDecode GetUpdateFromTable(NetworkObject NWO)
    {
        return SelectAllWhere(NWO.GetTableName(), $"`{NWO.GetIDCollumName()}`='{NWO.GetID()}'");
    }
}
