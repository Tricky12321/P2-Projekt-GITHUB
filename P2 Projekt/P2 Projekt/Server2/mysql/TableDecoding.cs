using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;


public class TableDecode
{
    public List<Row> RowData = new List<Row>();

    public int Count => RowData.Count;

    public TableDecode(MySqlDataReader Reader)
    {
        if (Reader != null)
        {
            int row = 0;
            //true og læser så længe den kan
            while (Reader.Read())
            {
                RowData.Add(new Row());
                //fieldcount = kolonner
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    //DBNull.value = null
                    //getname nenvet på kolonne, gestring værdi
                    RowData[row].Colums.Add(Reader[i].Equals(DBNull.Value) ? String.Empty : Reader.GetName(i));     // Navnet på den kolonne man henter
                    RowData[row].Values.Add(Reader[i].Equals(DBNull.Value) ? String.Empty : Reader.GetString(i));   // Værdien på den kolonne man henter
                }
                row++;
            }
        }

    }
}