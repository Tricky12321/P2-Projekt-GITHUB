using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OS
{
    public static bool IsLinux
    {
        // Tjekker om variabler i Envoirment OSVersion.Platform stemmer overens med dem som er kendt fra linux.
        get
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128) || Environment.OSVersion.ToString().ToLower().Contains("linux") ;
        }
    }

    public static bool IsWindows
    {
        get
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 2) || Environment.OSVersion.ToString().ToLower().Contains("windows");
        }
    }
}