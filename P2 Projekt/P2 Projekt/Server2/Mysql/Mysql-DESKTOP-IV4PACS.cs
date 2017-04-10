using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

public static class mySQL
{
    // IP -> Hvis Linux, brug 127.0.0.1 ellers 172.25.11.120 
    //(Hvis det er linux, er mysql databasen på localhost (127.0.0.1) ellers brug public IP 172.25.11.120)
    private static string _publicIP = "172.25.11.120";
    private static string _localIP = "127.0.0.1";
    // ---------------------------------------------------------------------------------------------------
    private static string _username = "connector";          // Ny bruger, så vi ikke bruger root
    private static string _password = "hSvi97RQMUaf7m9o";   // Tilfældig adgangskode, så den ikke bliver gættet
    private static string _database = "bussystem";          // Den database, som der skal forbindes til
    // ---------------------------------------------------------------------------------------------------
    private static string _ip => OS.IsLinux ? _localIP : _publicIP;
    private static string _connectionString => $"SERVER={_ip};uid={_username};PASSWORD={_password};DATABASE={_database};"; // Den streng, som indeholder alt 
    private static MySqlConnection _sqlConnect = new MySqlConnection(_connectionString); // Definere den forbindelse til databasen
    public static bool Connected;
    private static bool _firstConnect;
    // ---------------------------------------------------------------------------------------------------
    public static void Start()
    {
        Console.WriteLine("mySQL connection:");
        Console.WriteLine($"{_username}@{_ip}:3306");
        Console.Write("Connecting...");
        Connect();
        Print.PrintSuccessFailedLine(Connected);
        Print.PrintLine(ConsoleColor.Green);
    }

    public static void StartmySQL()
    {
        Thread mySQLThread = new Thread(new ThreadStart(mySQL.Start));
        mySQLThread.Start();
        // Venter på at MYSQL har forbindelse.
        // Kør INGEN andre kommandoer før der er forbindelse til MYSQL
        Utilities.WaitFor(ref mySQL.Connected);
    }

    public static bool Connect()
    {
        try
        {
            // Åben en forbindelse
            _sqlConnect.Open();
            Connected = true;
            _firstConnect = true;
            return true;
        }
        catch (MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                    throw new ConnectionFailedException("Connection Failed/Acces Denied\n" + ex.ToString());
                case 1045:
                    throw new ConnectionFailedException("Invalid Username/Password\n" + ex.ToString());
            }
            return false;
        }
        finally
        {
            _sqlConnect.Close();
        }
    }

    public static bool CheckConnection()
    {
        if (!_firstConnect)
        {
            throw new NotConnectedException("There is no connection made to the Database");
        }
        else
        {
            return true;
        }
    }

    public static bool RunQuery(string Query)
    {
        try
        {
            MySqlCommand cmd = _sqlConnect.CreateCommand();
            cmd.CommandText = Query;
            _sqlConnect.Open();
            cmd.ExecuteReader();
            Console.WriteLine($"{_sqlConnect.ConnectionString}");
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _sqlConnect.Close();
        }
        return true;
    }

    public static bool Insert(string table, string[] colums, string[] values)
    {
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
        RunQuery(TotalQuery);
        return true;

    }
}

public class ConnectionFailedException : Exception
{
    public ConnectionFailedException()
    {

    }

    public ConnectionFailedException(string message) : base(message)
    {

    }

    public ConnectionFailedException(string message, Exception inner) : base(message, inner)
    {

    }
}

public class NotConnectedException : Exception
{
    public NotConnectedException()
    {

    }

    public NotConnectedException(string message) : base(message)
    {

    }

    public NotConnectedException(string message, Exception inner) : base(message, inner)
    {

    }
}