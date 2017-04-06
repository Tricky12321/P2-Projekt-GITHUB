using System;
using System.Text;

public static class Print
{
    public static void PrintLine(ConsoleColor Color)
    {
        PrintColorLine("------------------------------", ConsoleColor.Green);
    }

    public static void PrintColorLine(string Text, ConsoleColor ForegroundColor)
    {
        PrintColor(Text + "\n", ForegroundColor, ConsoleColor.Black);
    }
        
    public static void PrintColor(string Text, ConsoleColor ForegroundColor)
    {
        PrintColor(Text, ForegroundColor, ConsoleColor.Black);
    }

    public static void PrintColor(string Text, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor)
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;
        Console.Write(Text);
        Console.ResetColor();
    }

    public static void PrintSuccessFailed(bool Value)
    {
        PrintColorLine(Value ? "Successfull!" : "Failed!" , Value ? ConsoleColor.Green : ConsoleColor.Red);

    }

    public static void PrintSuccessFailedLine(bool Value)
    {
        PrintColor(Value ? "Successfull!\n" : "Failed!\n", Value ? ConsoleColor.Green : ConsoleColor.Red);

    }

    public static void PrintTrueFalse(bool Value)
    {
        PrintColor(Value.ToString(), Value ? ConsoleColor.Green : ConsoleColor.Red);
    }

    public static void PrintTrueFalseLine(bool Value)
    {
        PrintColorLine(Value.ToString(), Value ? ConsoleColor.Green : ConsoleColor.Red);
    }
}
