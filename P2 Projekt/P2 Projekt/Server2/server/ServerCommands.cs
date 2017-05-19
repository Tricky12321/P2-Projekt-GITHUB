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
                    day Ugedag = (day)1;
                    Testbus.GetUpdate();
                    SimBus TestBusSim = new SimBus(Testbus, Ugedag);
                }
                break;
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
            case "test123":
                RealClient PassiveClient = new RealClient();
                PassiveClient.RequestAllWhere(ObjectTypes.Bus, "None");
                break;
            default:
                Print.PrintColorLine("Unknown Command!", ConsoleColor.Red);
                break;
        }
        WaitForCommand();

    }
}
