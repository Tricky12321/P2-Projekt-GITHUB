using System;
using System.Collections.Generic;
using System.Threading;
using JsonSerializer;


public static class Program
{
    public static bool ExitProgramBool = false;

    public static int Main(String[] args)
    {
        // Printer lige om det er linux eller ej
        Utilities.CheckOS();
        // 
        new Thread(new ThreadStart(StartAll)).Start();
        ListenForCommands();
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
        // Venter lige 500 ms for at give Console et øjeblik til at printe. 
        Thread.Sleep(500);
        // Start så IPv4 og/eller IPv6
        Server.StartServer(true, true);
        JsonCache.StartThreads();
    }

    public static void ListenForCommands()
    {
        // Venter på at alle servere er startet, og at der er forbindelse til database.
        Utilities.WaitFor(ref Mysql.Connected);
        Utilities.WaitFor(ref Server.IPV4Started);
        Utilities.WaitFor(ref Server.IPV6Started);
        ServerCommands.WaitForCommand();
    }

}