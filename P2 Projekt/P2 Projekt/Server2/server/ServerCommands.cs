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
        Thread.Sleep(2000);
        Console.Write("> ");
        string command = Console.ReadLine();
        int k = 0;
        Client TestClient;
        Thread TestClientThread;
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
                Bus Testbus = new Bus();
                Testbus.BusID = 1;
                Testbus.GetUpdate();
                SimBus TestBusSim = new SimBus(Testbus);
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
                Print.PrintColorLine("Unknown Command!",ConsoleColor.Red);
                break;
        }
        Thread.Sleep(1000);
        WaitForCommand();
    }
}
