using System;

using System.IO;
using System.Collections.Generic;
using System.Runtime.Remoting;
public static class SynchronousSocketListener
{

    public static List<NetworkObject> ClassesToHandle = new List<NetworkObject>();
    // Incoming data from the client.  

    public static int Main(String[] args)
    {
        /*
        Person p = new Person();
        p.Name = "Thue";
        p.Efternavn = "Iversen";
        Type OrigiType = p.GetType();
        string jsonSerial = Json.Serialize(p);
        Type type = Json.GetTypeFromString(jsonSerial);
        // dynamic v2 = type.GetType().GetProperty("Value").GetValue(type, null);
        object v3 = Json.Deserialize(jsonSerial);
        Type NewType = v3.GetType();
        (v3 as NetworkObject).Start();
        */

        Server SocketServer = new Server(12943);
        SocketServer.StartListening();
        Console.ReadKey();
        return 0;
    }
}



public class NoObjectFound : Exception
{

}

public class Person : SendTimer, NetworkObject
{
    public string Name;
    public string Efternavn;

    public void Start()
    {
        Console.WriteLine("Person:");
        Console.WriteLine($"{Name}\n{Efternavn}");
        CalcDiff();
        Console.WriteLine($"{TimeDiff} ms");
    }


}


public class Car : SendTimer, NetworkObject
{
    public string Brand;
    public bool Benzin;
    public bool Diezel;
    public uint Passangers;

    public void Start()
    {
        CalcDiff();
        Console.WriteLine("Car");
        Console.WriteLine($"{Brand.ToString()}");
        Console.WriteLine($"{Benzin.ToString()}");
        Console.WriteLine($"{Diezel.ToString()}");
        Console.WriteLine($"{Passangers.ToString()}");
        Console.WriteLine($"{TimeDiff}");
    }
}

public class SendTimer
{
    public double TimeDiff;
    public SendTimer()
    {
        // PrintTime();
        // Time = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }

    public void CalcDiff()
    {
        TimeDiff = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds - Time;
    }

    public double Time;
}

public interface NetworkObject
{
    void Start();
}

