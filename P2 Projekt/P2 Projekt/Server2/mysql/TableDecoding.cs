﻿using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;


public class TableDecode
{
    public List<Row> RowData = new List<Row>();
    public TableDecode(MySqlDataReader Reader)
    {
        if (Reader != null)
        {
            int row = 0;
            while (Reader.Read())
            {
                RowData.Add(new Row());
                for (int i = 0; i < Reader.FieldCount; i++)
                {
                    RowData[row].Colums.Add(Reader[i].Equals(DBNull.Value) ? String.Empty : Reader.GetName(i));     // Navnet på den kolonne man henter
                    RowData[row].Values.Add(Reader[i].Equals(DBNull.Value) ? String.Empty : Reader.GetString(i));   // Værdien på den kolonne man henter
                }
                row++;
            }
        }

    }
}