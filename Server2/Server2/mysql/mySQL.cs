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
            return true;
        }
        catch (MySqlException ex)
        {
            switch (ex.Number)
            {
                case 0:
                    throw new ConnectionFailedException("Connection Failed/Acces Denied\n"+ex.ToString());
                case 1045:
                    throw new ConnectionFailedException("Invalid Username/Password\n" + ex.ToString());
                
                
            }
            return false;
        }
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