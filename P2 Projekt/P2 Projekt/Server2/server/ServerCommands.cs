using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
                Client TestClient = new Client();
                TestClient.SendTestObject();
                break;
            default:
                break;
        }
        WaitForCommand();
    }
}
