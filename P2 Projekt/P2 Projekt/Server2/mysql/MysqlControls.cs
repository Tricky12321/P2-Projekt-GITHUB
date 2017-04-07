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
        // TODO: Skal optimeres... meget langsom...
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
}
