using System;
using System.Collections.Generic;
using System.Threading;

public static class Program
{
    public static List<NetworkObject> ClassesToHandle = new List<NetworkObject>();

    public static int Main(String[] args)
    {
        // Printer lige om det er linux eller ej
        Utilities.CheckOS();
        StartAll();
        // RunTest();
        Console.ReadKey(); // Readkey fordi programmet helst ikke bare skulle stoppe. 
        return 0;
    }

    public static void StartAll()
    {       
        /* Starter så alle servere i den her række følge
        * 1. MYSQL Forbindelse (connector@public/private ip)
        * 2. IPv4 (172.25.11.120/127.0.0.1/0.0.0.0)
        * 3. IPv6 (0000:0000:0000:0000:0000:0000:0000:0000/FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF/::)
        */
        // Start mySQL serveren først!
        Mysql.StartmySQL();
        // Start så IPv4 og/eller IPv6
        Server.StartServer(true, true);
    }

    public static void RunTest()
    {
        Thread.Sleep(2000);
        Console.WriteLine("Waiting 2 sec before starting test");
        Thread.Sleep(2000);
        /*
        string[] Colums = new string[] {"server_os", "function", "description"};
        string[] Values = new string[] { Utilities.GetOS(), "RunTests", "Køre test på Programmet"};
        mySQL.Insert("logging", Colums, Values);
        */
        Mysql.RunTest();
        Console.WriteLine("Testing Done!");
    }
}