using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Threading;

public static class Mysql
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
    private static string _ip => OS.IsLinux ? _localIP : _publicIP; // Hvis OS er linux, skal den bruge lokal IP (127.0.0.1)
    private static string _connectionString => $"SERVER={_ip};uid={_username};PASSWORD={_password};DATABASE={_database};"; // Den streng, som indeholder alt 
    private static MySqlConnection _sqlConnect = new MySqlConnection(_connectionString); // Definere den forbindelse til databasen
    public static bool Connected;                           // True hvis der er forbindelse til databasen, da serveren startede
    private static bool _firstConnect;                      // Bliver sat til true, hvis det er første gang. 
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
        Thread mySQLThread = new Thread(new ThreadStart(Mysql.Start));
        mySQLThread.Start();
        // Venter på at MYSQL har forbindelse.
        // Kør INGEN andre kommandoer før der er forbindelse til MYSQL
        Utilities.WaitFor(ref Mysql.Connected);
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

    public static bool RunQuery(string Query, bool NoLog = false)
    {
        if (!NoLog)
        {
            string LogQuery = Query;
            if (Query.Length > 50)
            {
                LogQuery = Query.Substring(0, 45);
                LogQuery += "...";

            }
                Log.LogData("RunQuery", $"{LogQuery} blev kørt");

        }

        try
        {
            MySqlCommand cmd = _sqlConnect.CreateCommand();
            cmd.CommandText = Query;
            _sqlConnect.Open();
            cmd.ExecuteNonQuery();
            // Console.WriteLine($"{_sqlConnect.ConnectionString}");
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

    public static TableDecode RunQueryWithReturn(string Query)
    {
        
        Log.LogData("RunQueryWIthReturn", $"{Query} blev kørt");
        TableDecode TableContent;
        try
        {
            // Hvilken commando skal der køres (Query)
            MySqlCommand cmd = _sqlConnect.CreateCommand();
            cmd.CommandText = Query;
            // Åbner forbindelsen til databasen (OPEN)
            _sqlConnect.Open();
            MySqlDataReader Reader = cmd.ExecuteReader();
            // Sikre sig at der er noget at hente i databasen.
            if (!Reader.HasRows)
            {
                // throw new EmptyTableException("The tabel is empty, are you sure this is what you wanted?");
            }
            TableContent = new TableDecode(Reader);
            Reader.Close();

        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            // Sørger for at vi lukker mysql forbindelsen
            _sqlConnect.Close();
        }
        return TableContent;
    }

    public static void RunTest()
    {
        TableDecode Output = RunQueryWithReturn("SELECT * FROM `logging`");
    }
}
