using System;
using System.Threading;
using ServerGPSSimulering;
using System.Collections.Generic;


public static class ServerCommands
{
    private static void WatiForESC()
    {
        Print.PrintCenterColor("Press ", "ESC", " to enter commands", ConsoleColor.DarkMagenta);
        do
        {
            while (!Console.KeyAvailable)
            {
                // Vent...
            }
        }
        while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }

    public static void WaitForCommand()
    {
        Thread.Sleep(1000);
        // Lytter efter om der bliver trykket ESC, hvis dette bliver gjort, går man ind i Command mode, og der vil ikke blive skrevet ud på STDIO
        WatiForESC();

        string command;
        lock (Print.ConsoleWriterLock)
        {
            Console.Write("\n> ");
            command = Console.ReadLine();
        }
        switch (command.ToLower())
        {
            case "testbus":
                lock (Print.ConsoleWriterLock)
                {
                    Bus Testbus = new Bus();
                    Testbus.BusID = 30;
                    //Convert.ToInt32(Console.ReadLine());
                    //day Ugedag = (day)Convert.ToInt32(Console.ReadLine());
                    day Ugedag = (day)1;
                    Testbus.GetUpdate();
                    SimBus TestBusSim = new SimBus(Testbus, Ugedag);
                }
                break;
            // Simulateweek simulere en hel uges data for en bus. 
            /*case "simulateweek":
                lock (Print.ConsoleWriterLock)
                {
                    Bus Testbus1 = new Bus();
                    Testbus1.BusID = 30;
                    Testbus1.GetUpdate();
                    SimBus Mandag = new SimBus(Testbus1, (day)1, false);
                    Bus Testbus2 = new Bus();
                    Testbus2.BusID = 31;
                    Testbus2.GetUpdate();
                    SimBus Tirsdag = new SimBus(Testbus2, (day)2, false);
                    Bus Testbus3 = new Bus();
                    Testbus3.BusID = 32;
                    Testbus3.GetUpdate();
                    SimBus Onsdag = new SimBus(Testbus3, (day)3, false);
                    Bus Testbus4 = new Bus();
                    Testbus4.BusID = 33;
                    Testbus4.GetUpdate();
                    SimBus Torsdag = new SimBus(Testbus4, (day)4, false);
                    Bus Testbus5 = new Bus();
                    Testbus5.BusID = 34;
                    Testbus5.GetUpdate();
                    SimBus Fredag = new SimBus(Testbus5, (day)5, false);
                    Bus Testbus6 = new Bus();
                    Testbus6.BusID = 35;
                    Testbus6.GetUpdate();
                    SimBus Lørdag = new SimBus(Testbus6, (day)6, false);
                    Bus Testbus7 = new Bus();
                    Testbus7.BusID = 36;
                    Testbus7.GetUpdate();
                    SimBus Søndag = new SimBus(Testbus7, (day)7, false);
                }
                break;*/
            case "realclient":
                RealClient TestClient = new RealClient();
                Bus TestBus = new Bus();
                TestBus.BusID = 30;
                TestBus.GetUpdate();
                TestClient.SendObject(TestBus, typeof(Bus));
                break;
            case "exit":
            case "quit":
                Print.PrintColorLine("Program killed [0]", ConsoleColor.Red);
                Environment.Exit(0);
                Program.ExitProgramBool = true;
                break;
            case "fuck":
                enfuckingfunktion();
                break;
            default:
                Print.PrintColorLine("Unknown Command!", ConsoleColor.Red);
                break;
        }
        WaitForCommand();

    }

    public static void enfuckingfunktion()
    {
        Rute TestRute;

        // Stoppesteder til rute 2 med Storvorde //
        Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/);
        Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441)                                      /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/);
        Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364)                                    /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/);
        Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831)                                             /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/);
        Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901)                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 17)), new BusTidCombi(bus3, new Tidspunkt(12, 17))*/);
        Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 18)), new BusTidCombi(bus3, new Tidspunkt(12, 18))*/);

        TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph);

        Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute,
            (new StoppestedMTid(TestRute.StoppeSteder[0], new AfPåTidCombi())),
            (new StoppestedMTid(TestRute.StoppeSteder[1], new AfPåTidCombi())),
            (new StoppestedMTid(TestRute.StoppeSteder[2], new AfPåTidCombi())),
            (new StoppestedMTid(TestRute.StoppeSteder[3], new AfPåTidCombi())),
            (new StoppestedMTid(TestRute.StoppeSteder[4], new AfPåTidCombi())),
            (new StoppestedMTid(TestRute.StoppeSteder[5], new AfPåTidCombi())));

        /* Historik til alle 6 stoppesteder */
        TestBus.StoppeStederMTid[0].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30));
        TestBus.StoppeStederMTid[1].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 10), 4, 30));
        TestBus.StoppeStederMTid[2].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 20), 6, 30));
        TestBus.StoppeStederMTid[3].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 30), 8, 30));
        TestBus.StoppeStederMTid[4].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 40), 10, 30));
        TestBus.StoppeStederMTid[5].AfPåTidComb[0] = (new AfPåTidCombi(0, 2, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 50), 12, 30));

        /* Dagens rute, hvor bussen har besøgt 5 stoppesteder */
        TestBus.StoppeStederMTid[0].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)2, 1, new Tidspunkt(10, 00), 2, 30));
        TestBus.StoppeStederMTid[1].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)2, 1, new Tidspunkt(10, 10), 4, 30));
        TestBus.StoppeStederMTid[2].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[2], TestBus, (day)2, 1, new Tidspunkt(10, 20), 6, 30));
        TestBus.StoppeStederMTid[3].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[3], TestBus, (day)2, 1, new Tidspunkt(10, 30), 8, 30));
        TestBus.StoppeStederMTid[4].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[4], TestBus, (day)2, 1, new Tidspunkt(10, 40), 10, 30));
        TestBus.StoppeStederMTid[5].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[5], TestBus, (day)2, 1, new Tidspunkt(10, 50), 12, 30));

        Bus ForventetBus = TestBus;



        ForventetBus.StoppeStederMTid[5].AfPåTidComb[1].ForventetPassagere = 10;


        List<AfPåTidCombi> TestAfPåTidCombi = new List<AfPåTidCombi>();

        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[0].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[1].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[2].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[3].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[4].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[5].AfPåTidComb[0]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[0].AfPåTidComb[1]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[1].AfPåTidComb[1]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[2].AfPåTidComb[1]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[3].AfPåTidComb[1]);
        TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[4].AfPåTidComb[1]);

        bool yes = (Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere == 12);
        Print.WriteLine($"{Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere}");
        Print.WriteLine($"{yes}");
        //Console.ReadKey();
    }
}
