using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public static class ServerCommands
{
    public static void WaitForCommand()
    {
        Console.Write("> ");
        string command = Console.ReadLine();
        switch (command.ToLower())
        {
            case "test":
                //Mysql.RunTest();
                // Laver en ny tråd til at køre den virtuelle klient i, dette sikre at serveren køre som den skal og ikke bliver langsom. 
                Client TestClient = new Client();
                Thread TestClientThread = new Thread(new ThreadStart(TestClient.SendTestObject));
                TestClientThread.Start();
                break;
            default:
                break;
        }
        WaitForCommand();
    }
}
