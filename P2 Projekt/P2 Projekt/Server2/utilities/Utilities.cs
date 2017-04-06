using System;
public static class Utilities
{
    public static void WaitFor(ref bool Variable, bool State)
    {
        // Køre et while loop, indtil at variablen opfylder State
        while (Variable != State)
        {

        }
    }

    public static void WaitFor(ref bool Variable)
    {
        WaitFor(ref Variable, true);
    }

    public static void CheckOS()
    {
        Console.Write($"OS: ");
        Print.PrintColorLine(OS.IsLinux ? "Linux" : OS.IsWindows ? "Windows" : "Unknown" , ConsoleColor.Yellow);
        Print.PrintLine(ConsoleColor.Green);
    }
}