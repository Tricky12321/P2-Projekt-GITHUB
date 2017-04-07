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
        /* Starter så alle servere i den her række følge
        * 1. MYSQL Forbindelse (connector@public/private ip)
        * 2. IPv4 (172.25.11.120/127.0.0.1/0.0.0.0)
        * 3. IPv6 (0000:0000:0000:0000:0000:0000:0000:0000/FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF:FFFF/::)
        */
        StartAll();
        RunTest();
        Console.ReadKey();
        return 0;
    }

    public static void StartAll()
    {
        #region Comments
        /*
        Person p = new Person();
        p.Name = "Thue";
        p.Efternavn = "Iversen";
        Type OrigiType = p.GetType();
        string jsonSerial = Json.Serialize(p);
        Type type = Json.GetTypeFromString(jsonSerial);
        // dynamic v2 = type.GetType().GetProperty("Value").GetValue(type, null);
        object v3 = Json.Deserialize(jsonSerial);
        Type NewType = v3.GetType();
        (v3 as NetworkObject).Start();
        */
        //Server SocketServerIPV6 = new Server(12943, ServerType.Ipv6);
        //Thread IpV6Thread = new Thread(new ThreadStart(SocketServerIPV6.StartListening));
        //IpV6Thread.Start();
        #endregion
        // Start mySQL serveren først!
        mySQL.StartmySQL();
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
        mySQL.RunTest();
        Console.WriteLine("Testing Done!");
    }
}