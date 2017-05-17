using System;
using System.Threading;
using ServerGPSSimulering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


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
                AlgoritmeTest2();
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
        Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309));                                            
        Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441));
        Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364));
        Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831));
        Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901));
        Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311));
        Stoppested stopMadsFuckShit = new Stoppested("Stop Mads Fuck Shit", 7, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit2 = new Stoppested("Stop Mads Fuck Shit", 8, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit3 = new Stoppested("Stop Mads Fuck Shit", 9, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit4 = new Stoppested("Stop Mads Fuck Shit", 10, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit5 = new Stoppested("Stop Mads Fuck Shit", 11, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit6 = new Stoppested("Stop Mads Fuck Shit", 12, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit7 = new Stoppested("Stop Mads Fuck Shit", 13, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit8 = new Stoppested("Stop Mads Fuck Shit", 14, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit9 = new Stoppested("Stop Mads Fuck Shit", 15, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit10 = new Stoppested("Stop Mads Fuck Shit", 16, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit11 = new Stoppested("Stop Mads Fuck Shit", 17, new GPS(57.053136, 9.706311));

        TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph,
                                                                            stopMadsFuckShit,
                                                                            stopMadsFuckShit2,
                                                                            stopMadsFuckShit3,
                                                                            stopMadsFuckShit4

                                                                            );

        Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute);
        List<AfPåTidCombi> HistoryData = new List<AfPåTidCombi>();
        /* Historik til alle 6 stoppesteder */
        AfPåTidCombi NyHis;
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30);
        NyHis.ID = 8001;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 05), 4, 30);
        NyHis.ID = 8002;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 3, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 10), 7, 30);
        NyHis.ID = 8003;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 15), 8, 30);
        NyHis.ID = 8004;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 20), 8, 30);
        NyHis.ID = 8005;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(2, 0, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 25), 6, 30);
        NyHis.ID = 8006;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[6], TestBus, (day)1, 1, new Tidspunkt(10, 30), 5, 30);
        NyHis.ID = 8007;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[7], TestBus, (day)1, 1, new Tidspunkt(10, 35), 6, 30);
        NyHis.ID = 8008;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[8], TestBus, (day)1, 1, new Tidspunkt(10, 35), 7, 30);
        NyHis.ID = 8009;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[9], TestBus, (day)1, 1, new Tidspunkt(10, 35), 8, 30);
        NyHis.ID = 8010;
        HistoryData.Add(NyHis);
        /*
        NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[10], TestBus, (day)1, 1, new Tidspunkt(10, 35), 12, 30);
        NyHis.ID = 8011;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[11], TestBus, (day)1, 1, new Tidspunkt(10, 35), 14, 30);
        NyHis.ID = 8012;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[12], TestBus, (day)1, 1, new Tidspunkt(10, 35), 16, 30);
        NyHis.ID = 8013;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[13], TestBus, (day)1, 1, new Tidspunkt(10, 35), 20, 30);
        NyHis.ID = 8014;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[14], TestBus, (day)1, 1, new Tidspunkt(10, 35), 22, 30);
        NyHis.ID = 8015;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[15], TestBus, (day)1, 1, new Tidspunkt(10, 35), 21, 30);
        NyHis.ID = 8016;
        HistoryData.Add(NyHis);
        */



        StoppestedMTid NytStopMTid;

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[0];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 00), 4, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9001;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 2;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[1];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 05), 6, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9002;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 3;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[2];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 10), 10, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9003;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[3];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 15), 12, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9004;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[4];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 20), 13, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9005;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 13;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[5];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 14, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9006;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 14;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        /*
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[6];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 12, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9007;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[7];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 10, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9008;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 10;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[8];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 9, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9009;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 9;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[9];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9010;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 7, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9011;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[11];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9012;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[12];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[12], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[12].AfPåTidComb[0].ID = 9013;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[13];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[13], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[13].AfPåTidComb[0].ID = 9014;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[14];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[14], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[14].AfPåTidComb[0].ID = 9015;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[15];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[15], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[15].AfPåTidComb[0].ID = 9016;
        */

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[6];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[6], TestBus, (day)2, 1, new Tidspunkt(10, 30), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[6].AfPåTidComb[0].ID = 9007;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[7];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[7], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[7].AfPåTidComb[0].ID = 9008;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[8];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[8], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[8].AfPåTidComb[0].ID = 9009;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[9];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[9], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[9].AfPåTidComb[0].ID = 9010;
        /*
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9011;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9012;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9013;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9014;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9015;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9016;
        
        */
        /* Dagens rute, hvor bussen har besøgt 5 stoppesteder */
        /*
        TestBus.StoppeStederMTid[0].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)2, 1, new Tidspunkt(10, 00), 2, 30));
        TestBus.StoppeStederMTid[1].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)2, 1, new Tidspunkt(10, 10), 4, 30));
        TestBus.StoppeStederMTid[2].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[2], TestBus, (day)2, 1, new Tidspunkt(10, 20), 6, 30));
        TestBus.StoppeStederMTid[3].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[3], TestBus, (day)2, 1, new Tidspunkt(10, 30), 8, 30));
        TestBus.StoppeStederMTid[4].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[4], TestBus, (day)2, 1, new Tidspunkt(10, 40), 10, 30));
        Bus ForventetBus = TestBus;
        */



        //ForventetBus.StoppeStederMTid[5].AfPåTidComb[1].ForventetPassagere = 10;

        /*
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
        */

        Bus OuputBus = Algoritme.Algoritmen(TestBus, HistoryData, 6);
        /*
        Debug.Print(OuputBus.StoppeStederMTid[12].AfPåTidComb[0].ForventetPassagere.ToString());
        Debug.Print(OuputBus.StoppeStederMTid[13].AfPåTidComb[0].ForventetPassagere.ToString());
        Debug.Print(OuputBus.StoppeStederMTid[14].AfPåTidComb[0].ForventetPassagere.ToString());
        Debug.Print(OuputBus.StoppeStederMTid[15].AfPåTidComb[0].ForventetPassagere.ToString());
        */
        string output = OuputBus.StoppeStederMTid[6].AfPåTidComb[0].ForventetPassagere.ToString()+","+OuputBus.StoppeStederMTid[7].AfPåTidComb[0].ForventetPassagere.ToString()+","+OuputBus.StoppeStederMTid[8].AfPåTidComb[0].ForventetPassagere.ToString()+","+OuputBus.StoppeStederMTid[9].AfPåTidComb[0].ForventetPassagere.ToString();

        /*
        bool yes = (Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere == 12);
        Print.WriteLine($"{Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere}");
        Print.WriteLine($"{yes}");
        */
        //Console.ReadKey();
    }

    public static void AlgoritmeTest1()
    {
        Rute TestRute;

        // Stoppesteder til rute 2 med Storvorde //
        Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309));
        Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441));
        Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364));
        Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831));
        Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901));
        Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311));
        Stoppested stopMadsFuckShit = new Stoppested("Stop Mads Fuck Shit", 7, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit2 = new Stoppested("Stop Mads Fuck Shit", 8, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit3 = new Stoppested("Stop Mads Fuck Shit", 9, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit4 = new Stoppested("Stop Mads Fuck Shit", 10, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit5 = new Stoppested("Stop Mads Fuck Shit", 11, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit6 = new Stoppested("Stop Mads Fuck Shit", 12, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit7 = new Stoppested("Stop Mads Fuck Shit", 13, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit8 = new Stoppested("Stop Mads Fuck Shit", 14, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit9 = new Stoppested("Stop Mads Fuck Shit", 15, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit10 = new Stoppested("Stop Mads Fuck Shit", 16, new GPS(57.053136, 9.706311));

        TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph,
                                                                            stopMadsFuckShit,
                                                                            stopMadsFuckShit2,
                                                                            stopMadsFuckShit3,
                                                                            stopMadsFuckShit4,
                                                                            stopMadsFuckShit5,
                                                                            stopMadsFuckShit6,
                                                                            stopMadsFuckShit7,
                                                                            stopMadsFuckShit8,
                                                                            stopMadsFuckShit9,
                                                                            stopMadsFuckShit10
                                                                            );

        Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute);
        List<AfPåTidCombi> HistoryData = new List<AfPåTidCombi>();
        /* Historik til alle 6 stoppesteder */
        AfPåTidCombi NyHis;
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30);
        NyHis.ID = 8001;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 05), 4, 30);
        NyHis.ID = 8002;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 3, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 10), 7, 30);
        NyHis.ID = 8003;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 15), 8, 30);
        NyHis.ID = 8004;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 20), 8, 30);
        NyHis.ID = 8005;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(2, 0, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 25), 6, 30);
        NyHis.ID = 8006;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[6], TestBus, (day)1, 1, new Tidspunkt(10, 30), 5, 30);
        NyHis.ID = 8007;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[7], TestBus, (day)1, 1, new Tidspunkt(10, 35), 6, 30);
        NyHis.ID = 8008;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[8], TestBus, (day)1, 1, new Tidspunkt(10, 35), 7, 30);
        NyHis.ID = 8009;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[9], TestBus, (day)1, 1, new Tidspunkt(10, 35), 8, 30);
        NyHis.ID = 8010;
        HistoryData.Add(NyHis);

        NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[10], TestBus, (day)1, 1, new Tidspunkt(10, 35), 12, 30);
        NyHis.ID = 8011;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[11], TestBus, (day)1, 1, new Tidspunkt(10, 35), 14, 30);
        NyHis.ID = 8012;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[12], TestBus, (day)1, 1, new Tidspunkt(10, 35), 16, 30);
        NyHis.ID = 8013;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[13], TestBus, (day)1, 1, new Tidspunkt(10, 35), 20, 30);
        NyHis.ID = 8014;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[14], TestBus, (day)1, 1, new Tidspunkt(10, 35), 22, 30);
        NyHis.ID = 8015;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[15], TestBus, (day)1, 1, new Tidspunkt(10, 35), 21, 30);
        NyHis.ID = 8016;
        HistoryData.Add(NyHis);
        // ----- HISTORIE SLUT -----


        //------ DATA FRA IDAG ------
        StoppestedMTid NytStopMTid;

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[0];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 00), 4, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9001;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 2;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[1];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 05), 6, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9002;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 3;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[2];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 10), 10, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9003;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[3];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 15), 12, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9004;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[4];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 20), 13, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9005;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 13;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[5];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 14, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9006;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 14;
        TestBus.StoppeStederMTid.Add(NytStopMTid);

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[6];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 12, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9007;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[7];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 10, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9008;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 10;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[8];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 9, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9009;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 9;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[9];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9010;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 7, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9011;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[11];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9012;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[12];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[12], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[12].AfPåTidComb[0].ID = 9013;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[13];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[13], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[13].AfPåTidComb[0].ID = 9014;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[14];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[14], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[14].AfPåTidComb[0].ID = 9015;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[15];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[15], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[15].AfPåTidComb[0].ID = 9016;
        // ---------- Dags data slut ---------- 
        /*s

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[6];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[6], TestBus, (day)2, 1, new Tidspunkt(10, 30), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[6].AfPåTidComb[0].ID = 9007;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[7];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[7], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[7].AfPåTidComb[0].ID = 9008;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[8];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[8], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[8].AfPåTidComb[0].ID = 9009;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[9];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[9], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[9].AfPåTidComb[0].ID = 9010;
        /*
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9011;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9012;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9013;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9014;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9015;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9016;

        */
        /* Dagens rute, hvor bussen har besøgt 5 stoppesteder */
        /*
        TestBus.StoppeStederMTid[0].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)2, 1, new Tidspunkt(10, 00), 2, 30));
        TestBus.StoppeStederMTid[1].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)2, 1, new Tidspunkt(10, 10), 4, 30));
        TestBus.StoppeStederMTid[2].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[2], TestBus, (day)2, 1, new Tidspunkt(10, 20), 6, 30));
        TestBus.StoppeStederMTid[3].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[3], TestBus, (day)2, 1, new Tidspunkt(10, 30), 8, 30));
        TestBus.StoppeStederMTid[4].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[4], TestBus, (day)2, 1, new Tidspunkt(10, 40), 10, 30));
        Bus ForventetBus = TestBus;
        */



        //ForventetBus.StoppeStederMTid[5].AfPåTidComb[1].ForventetPassagere = 10;

        /*
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
        */

        Bus OuputBus = Algoritme.Algoritmen(TestBus, HistoryData, 12);
        StringBuilder NewStringBuilder = new StringBuilder();
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[12].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[13].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[14].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[15].AfPåTidComb[0].ForventetPassagere.ToString());
        string output = NewStringBuilder.ToString();
        /*
        bool yes = (Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere == 12);
        Print.WriteLine($"{Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere}");
        Print.WriteLine($"{yes}");
        */
        //Console.ReadKey();
    }

    public static void AlgoritmeTest2()
    {
        Rute TestRute;

        // Stoppesteder til rute 2 med Storvorde //
        Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309));
        Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441));
        Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364));
        Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831));
        Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901));
        Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311));
        Stoppested stopMadsFuckShit = new Stoppested("Stop Mads Fuck Shit", 7, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit2 = new Stoppested("Stop Mads Fuck Shit", 8, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit3 = new Stoppested("Stop Mads Fuck Shit", 9, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit4 = new Stoppested("Stop Mads Fuck Shit", 10, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit5 = new Stoppested("Stop Mads Fuck Shit", 11, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit6 = new Stoppested("Stop Mads Fuck Shit", 12, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit7 = new Stoppested("Stop Mads Fuck Shit", 13, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit8 = new Stoppested("Stop Mads Fuck Shit", 14, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit9 = new Stoppested("Stop Mads Fuck Shit", 15, new GPS(57.053136, 9.706311));
        Stoppested stopMadsFuckShit10 = new Stoppested("Stop Mads Fuck Shit", 16, new GPS(57.053136, 9.706311));

        TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                            stopFriluftsbadet,
                                                                            stopStenBillesGade,
                                                                            stopHaradlslund,
                                                                            stopSchleppegrellsgade,
                                                                            stopSctJoseph,
                                                                            stopMadsFuckShit,
                                                                            stopMadsFuckShit2,
                                                                            stopMadsFuckShit3,
                                                                            stopMadsFuckShit4
                                                                            );

        Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute);
        List<AfPåTidCombi> HistoryData = new List<AfPåTidCombi>();
        /* Historik til alle 6 stoppesteder */
        AfPåTidCombi NyHis;
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30);
        NyHis.ID = 8001;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 05), 4, 30);
        NyHis.ID = 8002;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 3, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 10), 7, 30);
        NyHis.ID = 8003;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 15), 8, 30);
        NyHis.ID = 8004;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 20), 8, 30);
        NyHis.ID = 8005;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(2, 0, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 25), 6, 30);
        NyHis.ID = 8006;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[6], TestBus, (day)1, 1, new Tidspunkt(10, 30), 5, 30);
        NyHis.ID = 8007;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[7], TestBus, (day)1, 1, new Tidspunkt(10, 35), 6, 30);
        NyHis.ID = 8008;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[8], TestBus, (day)1, 1, new Tidspunkt(10, 35), 7, 30);
        NyHis.ID = 8009;
        HistoryData.Add(NyHis);
        NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[9], TestBus, (day)1, 1, new Tidspunkt(10, 35), 8, 30);
        NyHis.ID = 8010;
        HistoryData.Add(NyHis);
        // ----- HISTORIE SLUT -----


        //------ DATA FRA IDAG ------
        StoppestedMTid NytStopMTid;

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[0];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 00), 4, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9001;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 2;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[1];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 05), 6, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9002;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 3;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[2];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 10), 10, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9003;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[3];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 15), 12, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9004;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[4];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 20), 13, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9005;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 13;
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[5];
        NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
        NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 14, 30));
        NytStopMTid.AfPåTidComb[0].ID = 9006;
        NytStopMTid.AfPåTidComb[0].ForventetPassagere = 14;
        TestBus.StoppeStederMTid.Add(NytStopMTid);

        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[6];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[6], TestBus, (day)2, 1, new Tidspunkt(10, 30), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[6].AfPåTidComb[0].ID = 9007;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[7];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[7], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[7].AfPåTidComb[0].ID = 9008;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[8];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[8], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[8].AfPåTidComb[0].ID = 9009;
        //------------------------------------------------
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[9];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[9], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[9].AfPåTidComb[0].ID = 9010;
        /*
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9011;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9012;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9013;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9014;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9015;
        NytStopMTid = new StoppestedMTid();
        NytStopMTid.Stop = TestRute.StoppeSteder[10];
        (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[10], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
        TestBus.StoppeStederMTid.Add(NytStopMTid);
        TestBus.StoppeStederMTid[10].AfPåTidComb[0].ID = 9016;

        */
        /* Dagens rute, hvor bussen har besøgt 5 stoppesteder */
        /*
        TestBus.StoppeStederMTid[0].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)2, 1, new Tidspunkt(10, 00), 2, 30));
        TestBus.StoppeStederMTid[1].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)2, 1, new Tidspunkt(10, 10), 4, 30));
        TestBus.StoppeStederMTid[2].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[2], TestBus, (day)2, 1, new Tidspunkt(10, 20), 6, 30));
        TestBus.StoppeStederMTid[3].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[3], TestBus, (day)2, 1, new Tidspunkt(10, 30), 8, 30));
        TestBus.StoppeStederMTid[4].AfPåTidComb.Add(new AfPåTidCombi(0, 2, TestRute.StoppeSteder[4], TestBus, (day)2, 1, new Tidspunkt(10, 40), 10, 30));
        Bus ForventetBus = TestBus;
        */



        //ForventetBus.StoppeStederMTid[5].AfPåTidComb[1].ForventetPassagere = 10;

        /*
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
        */

        Bus OuputBus = Algoritme.Algoritmen(TestBus, HistoryData, 12);
        StringBuilder NewStringBuilder = new StringBuilder();
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[6].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[7].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[8].AfPåTidComb[0].ForventetPassagere.ToString());
        NewStringBuilder.Append(",");
        NewStringBuilder.Append(OuputBus.StoppeStederMTid[9].AfPåTidComb[0].ForventetPassagere.ToString());
        string output = NewStringBuilder.ToString();
        //Assert.AreEqual("14,16,18,20", output);
        /*
        bool yes = (Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere == 12);
        Print.WriteLine($"{Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere}");
        Print.WriteLine($"{yes}");
        */
        //Console.ReadKey();
    }






}
