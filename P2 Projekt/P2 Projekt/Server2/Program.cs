using System;
using System.Collections.Generic;
using System.Threading;

public static class Program
{
    public static List<NetworkObject> ClassesToHandle = new List<NetworkObject>();

    public static void TestObject()
    {
        // Lavet et nyt Test Object
        Test TestObj = new Test();                                                  
        // Laver et nyt stopur
        System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch();    
        // Starter stopuret
        Timer.Start();                                                              
        //serialiseere objectet
        string JsonString = Json.Serialize(TestObj);                                
        // Stopper stopuret
        Timer.Stop();                                                               
        // Udskriver tiden
        Console.WriteLine($"Serialise: {Timer.ElapsedMilliseconds} ms");            
        // Genstarter stopuret
        Timer.Restart();                                                            
        // Deserialiserer objectet
        Test TestObjDe = Json.Deserialize(JsonString)[0] as Test;                      
        // Stopper stopuret
        Timer.Stop();                                                               
        // Udskriver tiden
        Console.WriteLine($"Deserialise: {Timer.ElapsedMilliseconds} ms");       
    }

    public static void TestRealClient()
    {
        RealClient TestClient = new RealClient();
        Bus TestBus = new Bus();
        /*
        TestBus.busID = 1;
        TestBus.busLok = new GPS();
        TestBus.busLok.xCoordinate = 54.2123;
        TestBus.busLok.yCoordinate = -21.2123;
        TestBus.busName = "2 Væddeløbsbanen";
        TestBus.CapacitySitting = 32;
        TestBus.CapacityStanding = 20;
        TestBus.besøgteStop = 123;
        TestBus.rute = new Rute();
        TestBus.rute.ruteName = "asdf";
        TestBus.rute.ruteID = 1;
        TestClient.SendObject(TestBus, typeof(Bus));
        */
        List<NetworkObject> NwO = TestClient.RequestAllWhere(ObjectTypes.Bus, "`ID`=1");

    }

    public static int Main(String[] args)
    {
        // Printer lige om det er linux eller ej
        Utilities.CheckOS();
        StartAll();
        RunTest();
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
        Thread.Sleep(1000);
        ServerCommands.WaitForCommand();
    }
}