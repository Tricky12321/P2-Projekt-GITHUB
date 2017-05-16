using System;
using System.Threading;
using ServerGPSSimulering;


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
            default:
                Print.PrintColorLine("Unknown Command!", ConsoleColor.Red);
                break;
        }
        WaitForCommand();

    }
}
