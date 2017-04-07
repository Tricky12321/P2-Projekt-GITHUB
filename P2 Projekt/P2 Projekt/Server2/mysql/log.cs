using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Log
{
    public static void LogData(string funcstring, string description)
    {
        MysqlControls.Insert("logging", new string[] { "server_os", "function", "description" }, new string[] { Utilities.GetOS(), funcstring, description });
    }

    public static void LogData(string description)
    {
        MysqlControls.Insert("logging", new string[] { "server_os", "function", "description" }, new string[] { Utilities.GetOS(), "NULL", description });
    }
}