using System;
using System.Collections.Generic;
using System.Threading;
using JsonSerializer;


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
        StartAll();
        RunTest();
        Console.ReadKey(); // Readkey fordi programmet helst ikke bare skulle stoppe. 
        return 0;
    }

    public static void TestBusSimulering()
    {
        // Stoppesteder til rute 2 med Storvorde //
        StoppestedMTid stopSkydebanevej = new StoppestedMTid(new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/));
        StoppestedMTid stopFriluftsbadet = new StoppestedMTid(new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441)                                       /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/));
        StoppestedMTid stopStenBillesGade = new StoppestedMTid(new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364)                                     /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/));
        StoppestedMTid stopHaradlslund = new StoppestedMTid(new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831)                                              /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/));
        StoppestedMTid stopSchleppegrellsgade = new StoppestedMTid(new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901)                                /* , new BusTidCombi(bus2, new Tidspunkt(11, 17)), new BusTidCombi(bus3, new Tidspunkt(12, 17))*/));
        StoppestedMTid stopSctJoseph = new StoppestedMTid(new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311)                                                /* , new BusTidCombi(bus2, new Tidspunkt(11, 18)), new BusTidCombi(bus3, new Tidspunkt(12, 18))*/));
        StoppestedMTid stopBadehusvej = new StoppestedMTid(new Stoppested("Badehusvej (Kastetvej / Aalborg)", 7, new GPS(57.052275, 9.911615)                                                /* , new BusTidCombi(bus2, new Tidspunkt(11, 20)), new BusTidCombi(bus3, new Tidspunkt(12, 20))*/));
        StoppestedMTid stopJumfruAneGade = new StoppestedMTid(new Stoppested("Jomfru And Gade (Borgergade / Aalborg)", 8, new GPS(57.051035, 9.918944)                                       /* , new BusTidCombi(bus2, new Tidspunkt(11, 21)), new BusTidCombi(bus3, new Tidspunkt(12, 21))*/));
        StoppestedMTid stopNytorv = new StoppestedMTid(new Stoppested("Nytorv (Østeraagade / Aalborg)", 9, new GPS(57.049890, 9.92249)                                                       /* , new BusTidCombi(bus2, new Tidspunkt(11, 23)), new BusTidCombi(bus3, new Tidspunkt(12, 23))*/));
        StoppestedMTid stopBoulevarden = new StoppestedMTid(new Stoppested("Boulevarden (Aalborg)", 10, new GPS(57.046287, 9.91882)                                                          /* , new BusTidCombi(bus2, new Tidspunkt(11, 25)), new BusTidCombi(bus3, new Tidspunkt(12, 25))*/));
        StoppestedMTid stopAalborgBusterminalOmrådeA = new StoppestedMTid(new Stoppested("Aalborg Busterminal (Område A)", 11, new GPS(57.042844, 9.917750)                                  /* , new BusTidCombi(bus2, new Tidspunkt(11, 30)), new BusTidCombi(bus3, new Tidspunkt(12, 30))*/));
        StoppestedMTid stopPolitigården = new StoppestedMTid(new Stoppested("Politigården (Jyllandsgade / Aalborg)", 12, new GPS(57.042459, 9.926213)                                        /* , new BusTidCombi(bus2, new Tidspunkt(11, 31)), new BusTidCombi(bus3, new Tidspunkt(12, 31))*/));
        StoppestedMTid stopBornholmsgade = new StoppestedMTid(new Stoppested("Bornholmsgade (Aalborg)", 13, new GPS(57.041973, 9.933989)                                                     /* , new BusTidCombi(bus2, new Tidspunkt(11, 33)), new BusTidCombi(bus3, new Tidspunkt(12, 33))*/));
        StoppestedMTid stopØstreAllé = new StoppestedMTid(new Stoppested("Østre Allé (Bornholmsgade / Aalborg)", 14, new GPS(57.038234, 9.935614)                                            /* , new BusTidCombi(bus2, new Tidspunkt(11, 35)), new BusTidCombi(bus3, new Tidspunkt(12, 35))*/));
        StoppestedMTid stopSohngårdsholmparken = new StoppestedMTid(new Stoppested("Sohngårdsholmsparken (Sohngårdsholmsvej / Aalborg)", 15, new GPS(57.032888, 9.943680)                    /* , new BusTidCombi(bus2, new Tidspunkt(11, 37)), new BusTidCombi(bus3, new Tidspunkt(12, 37))*/));
        StoppestedMTid stopMagisterparken = new StoppestedMTid(new Stoppested("Magisterparken (Sohngårdsholmsvej / Aalborg)", 16, new GPS(57.027143, 9.943908)                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 38)), new BusTidCombi(bus3, new Tidspunkt(12, 38))*/));
        StoppestedMTid stopGrønlandsTorv = new StoppestedMTid(new Stoppested("Grønlands Torv(Universitetsboulevarden / Aalborg)", 17, new GPS(57.024262, 9.942691)                           /* , new BusTidCombi(bus2, new Tidspunkt(11, 40)), new BusTidCombi(bus3, new Tidspunkt(12, 40))*/));
        StoppestedMTid stopScoresbysundvej = new StoppestedMTid(new Stoppested("Scoresbysundvej (Universitetsboulevarden / Aalborg)", 18, new GPS(57.021429, 9.951671)                       /* , new BusTidCombi(bus2, new Tidspunkt(11, 41)), new BusTidCombi(bus3, new Tidspunkt(12, 41))*/));
        StoppestedMTid stopPendlerpladsen = new StoppestedMTid(new Stoppested("Pendlerpladsen E45 (Bertil Ohlins Vej / Aalborg)", 19, new GPS(57.018545, 9.956784)                           /* , new BusTidCombi(bus2, new Tidspunkt(11, 42)), new BusTidCombi(bus3, new Tidspunkt(12, 42))*/));
        StoppestedMTid stopGigantium = new StoppestedMTid(new Stoppested("Gigantium (Bertil Ohlins Vej / Aalborg)", 20, new GPS(57.016861, 9.963055)                                         /* , new BusTidCombi(bus2, new Tidspunkt(11, 43)), new BusTidCombi(bus3, new Tidspunkt(12, 43))*/));
        StoppestedMTid stopPontoppidanstræde = new StoppestedMTid(new Stoppested("Pontoppidanstræde (Bertil Ohlins Vej / Aalborg)", 21, new GPS(57.015024, 9.972542)                         /* , new BusTidCombi(bus2, new Tidspunkt(11, 44)), new BusTidCombi(bus3, new Tidspunkt(12, 44))*/));
        StoppestedMTid stopAauKantinen = new StoppestedMTid(new Stoppested("AAU Kantinen (Bertil Ohlins Vej / Aalborg)", 22, new GPS(57.015536, 9.975977)                                    /* , new BusTidCombi(bus2, new Tidspunkt(11, 45)), new BusTidCombi(bus3, new Tidspunkt(12, 45))*/));
        StoppestedMTid stopAauKroghstræde = new StoppestedMTid(new Stoppested("AAU Kroghstræde (Bertil Ohlins Vej / Aalborg)", 23, new GPS(57.015597, 9.978854)                              /* , new BusTidCombi(bus2, new Tidspunkt(11, 45)), new BusTidCombi(bus3, new Tidspunkt(12, 45))*/));
        StoppestedMTid stopAauDamstræde = new StoppestedMTid(new Stoppested("AAU Damstræde (Bertil Ohlins Vej / Aalborg)", 24, new GPS(57.014884, 9.983080)                                  /* , new BusTidCombi(bus2, new Tidspunkt(11, 46)), new BusTidCombi(bus3, new Tidspunkt(12, 46))*/));
        StoppestedMTid stopMejrupstien = new StoppestedMTid(new Stoppested("Mejrupstien (Bertil Ohlins Vej / Aalborg)", 25, new GPS(57.014825, 9.987398)                                     /* , new BusTidCombi(bus2, new Tidspunkt(11, 47)), new BusTidCombi(bus3, new Tidspunkt(12, 47))*/));
        StoppestedMTid stopAauBusterminal = new StoppestedMTid(new Stoppested("AAU Busterminal (Sigrid Undsetsvej / Aalborg)", 26, new GPS(57.015584, 9.991391)                              /* , new BusTidCombi(bus2, new Tidspunkt(11, 48)), new BusTidCombi(bus3, new Tidspunkt(12, 48))*/));
        StoppestedMTid stopSpergelvej = new StoppestedMTid(new Stoppested("Spergelvej (Klarupvej / Klarup)", 27, new GPS(57.012756, 10.040549)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 53)), new BusTidCombi(bus3, new Tidspunkt(12, 53))*/));
        StoppestedMTid stopLucernevej = new StoppestedMTid(new Stoppested("Lucernevej (Klarupvek / Klarup)", 28, new GPS(57.011928, 10.043184)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 53)), new BusTidCombi(bus3, new Tidspunkt(12, 53))*/));
        StoppestedMTid stopPostvej = new StoppestedMTid(new Stoppested("Postvej (Klarupvej / Klarup)", 29, new GPS(57.012423, 10.045070)                                                     /* , new BusTidCombi(bus2, new Tidspunkt(11, 54)), new BusTidCombi(bus3, new Tidspunkt(12, 54))*/));
        StoppestedMTid stopLabyrinten = new StoppestedMTid(new Stoppested("Labyrinten (Klarupvej / Klarup)", 30, new GPS(57.014457, 10.051187)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 56)), new BusTidCombi(bus3, new Tidspunkt(12, 56))*/));
        StoppestedMTid stopSeptempervej = new StoppestedMTid(new Stoppested("Septempervej (Klarupvej / Klarup)", 31, new GPS(57.015687, 10.059013)                                           /* , new BusTidCombi(bus2, new Tidspunkt(11, 56)), new BusTidCombi(bus3, new Tidspunkt(12, 56))*/));
        StoppestedMTid stopKalanderparken = new StoppestedMTid(new Stoppested("Kalenderparken(Klarupvej / Klarup)", 32, new GPS(57.014075, 10.066294)                                        /* , new BusTidCombi(bus2, new Tidspunkt(11, 57)), new BusTidCombi(bus3, new Tidspunkt(12, 57))*/));
        StoppestedMTid stopVestermarksvej = new StoppestedMTid(new Stoppested("Vestermarksvej (Vandværksvej / Storvorde)", 33, new GPS(57.006666, 10.092691)                                 /* , new BusTidCombi(bus2, new Tidspunkt(12, 00)), new BusTidCombi(bus3, new Tidspunkt(13, 00))*/));
        StoppestedMTid stopVandværksvej = new StoppestedMTid(new Stoppested("Vandværksvej (Storvorde)", 34, new GPS(57.005224, 10.097992)                                                    /* , new BusTidCombi(bus2, new Tidspunkt(12, 01)), new BusTidCombi(bus3, new Tidspunkt(13, 01))*/));
        StoppestedMTid stopSmedevænget = new StoppestedMTid(new Stoppested("Smedevænget (Vandværksvej / Storvorde)", 35, new GPS(57.004656, 10.101815)                                       /* , new BusTidCombi(bus2, new Tidspunkt(12, 02)), new BusTidCombi(bus3, new Tidspunkt(13, 02))*/));
        StoppestedMTid stopErikJørgensensPlads = new StoppestedMTid(new Stoppested("Erik Jørgensens Plads(Storvorde)", 36, new GPS(57.001658, 10.101974)                                     /* , new BusTidCombi(bus2, new Tidspunkt(12, 03)), new BusTidCombi(bus3, new Tidspunkt(13, 03))*/));
        StoppestedMTid stopMarkvej = new StoppestedMTid(new Stoppested("Markvej (Storvorde)", 37, new GPS(57.001809, 10.095683)                                                              /* , new BusTidCombi(bus2, new Tidspunkt(12, 04)), new BusTidCombi(bus3, new Tidspunkt(13, 04))*/));
        StoppestedMTid stopRødhøjvej = new StoppestedMTid(new Stoppested("Rødhøjvej (Storvorde)", 38, new GPS(56.999864, 10.100411)                                                          /* , new BusTidCombi(bus2, new Tidspunkt(12, 05)), new BusTidCombi(bus3, new Tidspunkt(13, 05))*/));

        Rute rute2VædSto = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph,
                                                                            stopBadehusvej,
                                                                            stopJumfruAneGade,
                                                                            stopNytorv,
                                                                            stopBoulevarden,
                                                                            stopAalborgBusterminalOmrådeA,
                                                                            stopPolitigården,
                                                                            stopBornholmsgade,
                                                                            stopØstreAllé,
                                                                            stopSohngårdsholmparken,
                                                                            stopMagisterparken,
                                                                            stopGrønlandsTorv,
                                                                            stopScoresbysundvej,
                                                                            stopPendlerpladsen,
                                                                            stopGigantium,
                                                                            stopPontoppidanstræde,
                                                                            stopAauKantinen,
                                                                            stopAauKroghstræde,
                                                                            stopAauDamstræde,
                                                                            stopMejrupstien,
                                                                            stopAauBusterminal,
                                                                            stopSpergelvej,
                                                                            stopLucernevej,
                                                                            stopPostvej,
                                                                            stopLabyrinten,
                                                                            stopSeptempervej,
                                                                            stopKalanderparken,
                                                                            stopVestermarksvej,
                                                                            stopVandværksvej,
                                                                            stopSmedevænget,
                                                                            stopErikJørgensensPlads,
                                                                            stopMarkvej,
                                                                            stopRødhøjvej);

        Rute rute3VædSto = new Rute("Rute3 Væddeløbsbanen - Storvorde", 265, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph,
                                                                            stopBadehusvej,
                                                                            stopJumfruAneGade,
                                                                            stopNytorv,
                                                                            stopBoulevarden,
                                                                            stopAalborgBusterminalOmrådeA,
                                                                            stopPolitigården,
                                                                            stopBornholmsgade,
                                                                            stopØstreAllé,
                                                                            stopSohngårdsholmparken,
                                                                            stopMagisterparken,
                                                                            stopGrønlandsTorv,
                                                                            stopScoresbysundvej,
                                                                            stopPendlerpladsen,
                                                                            stopGigantium);

        Bus bus2 = new Bus("bus2", 123, 56, 47, rute2VædSto, (new StoppestedMTid(stopSkydebanevej, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopFriluftsbadet, (new AfPåTidCombi(new Tidspunkt(12, 01))), (new AfPåTidCombi(new Tidspunkt(15, 00))))),
                                                                (new StoppestedMTid(stopStenBillesGade, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopHaradlslund, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopSchleppegrellsgade, (new AfPåTidCombi(new Tidspunkt(12, 01))), (new AfPåTidCombi(new Tidspunkt(15, 00))))),
                                                                (new StoppestedMTid(stopSctJoseph, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopBadehusvej, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopJumfruAneGade, (new AfPåTidCombi(new Tidspunkt(12, 01))), (new AfPåTidCombi(new Tidspunkt(15, 00))))),
                                                                (new StoppestedMTid(stopNytorv, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopBoulevarden, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopAalborgBusterminalOmrådeA, (new AfPåTidCombi(new Tidspunkt(12, 01))), (new AfPåTidCombi(new Tidspunkt(15, 00))))),
                                                                (new StoppestedMTid(stopPolitigården, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopBornholmsgade, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopØstreAllé, (new AfPåTidCombi(new Tidspunkt(12, 01))), (new AfPåTidCombi(new Tidspunkt(15, 00))))),
                                                                (new StoppestedMTid(stopSohngårdsholmparken, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopMagisterparken, (new AfPåTidCombi(new Tidspunkt(12, 04))), (new AfPåTidCombi(new Tidspunkt(13, 00))))));

        Bus bus3 = new Bus("bus3", 234, 47, 10, rute3VædSto, (new StoppestedMTid(stopSkydebanevej, (new AfPåTidCombi(new Tidspunkt(12, 00))), (new AfPåTidCombi(new Tidspunkt(13, 00))))),
                                                                (new StoppestedMTid(stopSchleppegrellsgade, (new AfPåTidCombi(new Tidspunkt(14, 00))), (new AfPåTidCombi(new Tidspunkt(15, 00))))));

        Lists.listWithRoutes.Add(rute2VædSto);
        Lists.listWithRoutes.Add(rute3VædSto);

        foreach (StoppestedMTid stop in rute2VædSto.AfPåRuteListMTid)
        {
            Lists.listWithStops.Add(stop);
        }

        foreach (var singleStop in Lists.listWithStops)
        {
            //singleStop.UploadToDatabase();
        }
        Lists.listWithBusses.Add(bus2);
        Lists.listWithBusses.Add(bus3);
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
        TestRealClient();
        ServerCommands.WaitForCommand();
    }
}