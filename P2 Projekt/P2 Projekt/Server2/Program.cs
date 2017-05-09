using System;
using System.Collections.Generic;
using System.Threading;
using JsonSerializer;


public static class Program
{
    public static bool ExitProgramBool = false;

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
        Rute BusRute = new Rute();
        BusRute.RuteID = 1;
        BusRute.GetUpdate();
        /*
        RealClient TestClient = new RealClient();
        Bus TestBus = new Bus();
        
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
        
        List<NetworkObject> NwOs = TestClient.RequestAllWhere(ObjectTypes.BusStop, "");
        foreach (NetworkObject NwO in NwOs)
        {
            System.Diagnostics.Debug.Print(NwO.ToString());
        }
        */
    }

    public static int Main(String[] args)
    {
        // Printer lige om det er linux eller ej
        Utilities.CheckOS();
        Thread StartAllThread = new Thread(new ThreadStart(StartAll));
        Thread TestThread = new Thread(new ThreadStart(RunTest));
        StartAllThread.Start();
        TestThread.Start();
        while (!ExitProgramBool)
        {

        }
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
        Utilities.WaitFor(ref Mysql.Connected);
        Thread.Sleep(500);
        // Start så IPv4 og/eller IPv6
        Server.StartServer(true, true);
    }

    public static void RunBusTest()
    {
        Bus NyBus = new Bus();
        NyBus.BusID = 123;
        NyBus.GetUpdate();
        NyBus.BusID = 1;
        RealClient NewClient = new RealClient();
        NewClient.SendObject(NyBus, typeof(Bus));

    }

    public static void RunTest()
    {
        //RunBusTest();
        Utilities.WaitFor(ref Mysql.Connected);
        ServerCommands.WaitForCommand();
    }

    public static void BusTestTing()
    {
        Bus TestBus = (Json.Deserialize("")[0] as Bus);
    }
}