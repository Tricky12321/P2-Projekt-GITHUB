using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ServerGPSSimulering;


public static class ServerCommands
{
    public static void WaitForCommand()
    {
        Thread.Sleep(1000);
        Print.PrintCenterColor("Press ", "ESC", " to enter commands", ConsoleColor.DarkMagenta);
        do
        {
            while (!Console.KeyAvailable)
            {
                // Do something
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        string command;
        int k = 0;
        Client TestClient;
        Thread TestClientThread;
        lock (Print.ConsoleWriterLock)
        {
            Console.Write("\n> ");
            command = Console.ReadLine();
        }
        switch (command.ToLower())
        {
            case "ultratest":
                for (int i = 0; i < 1000; i++)
                {
                    Print.PrintCenterColor("Test: ", k.ToString(), "#", ConsoleColor.Yellow);
                    k++;
                    TestClient = new Client();
                    TestClientThread = new Thread(new ThreadStart(TestClient.SendTestObject));
                    TestClientThread.Start();
                    Thread.Sleep(100);
                }
                break;
            case "hardtest":
                for (int i = 0; i < 100; i++)
                {
                    Print.PrintCenterColor("Test: ", k.ToString(), "#", ConsoleColor.Yellow);
                    k++;
                    TestClient = new Client();
                    TestClientThread = new Thread(new ThreadStart(TestClient.SendTestObject));
                    TestClientThread.Start();
                    Thread.Sleep(500);
                }
                break;
            case "middletest":
                for (int i = 0; i < 25; i++)
                {
                    Print.PrintCenterColor("Test: ", k.ToString(), "#", ConsoleColor.Yellow);
                    k++;
                    TestClient = new Client();
                    TestClientThread = new Thread(new ThreadStart(TestClient.SendTestObject));
                    TestClientThread.Start();
                    Thread.Sleep(1000);
                }
                break;
            case "test":

                Print.PrintCenterColor("Test: ", k.ToString(), "#", ConsoleColor.Yellow);
                k++;
                TestClient = new Client();
                TestClientThread = new Thread(new ThreadStart(TestClient.SendTestObject));
                TestClientThread.Start();
                //Mysql.RunTest();
                // Laver en ny tråd til at køre den virtuelle klient i, dette sikre at serveren køre som den skal og ikke bliver langsom. 
                break;
            case "testbus":
                lock (Print.ConsoleWriterLock)
                {
                    Bus Testbus = new Bus();
                    Testbus.BusID = Convert.ToInt32(Console.ReadLine());
                    day Ugedag = (day)Convert.ToInt32(Console.ReadLine());
                    Testbus.GetUpdate();
                    SimBus TestBusSim = new SimBus(Testbus, Ugedag);
                }
                break;
            case "oprettestbusser":
                Bus OrigBus = new Bus();
                OrigBus.BusID = 30;
                OrigBus.GetUpdate();
                OrigBus.BusID = 31;
                OrigBus.UploadToDatabase();
                OrigBus.BusID = 32;
                OrigBus.UploadToDatabase();
                OrigBus.BusID = 33;
                OrigBus.UploadToDatabase();
                OrigBus.BusID = 34;
                OrigBus.UploadToDatabase();
                OrigBus.BusID = 35;
                OrigBus.UploadToDatabase();
                OrigBus.BusID = 36;
                OrigBus.UploadToDatabase();
                break;

            case "simulateweek":
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
                break;
            case "realclient":
                Program.TestRealClient();
                break;
            case "exit":
            case "quit":
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
