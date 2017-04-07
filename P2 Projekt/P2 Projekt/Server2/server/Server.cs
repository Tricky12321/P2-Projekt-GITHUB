using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

public enum ServerType
{
    Ipv6, Ipv4
}
public class Server
{
    private uint _port;
    public uint GetPort => _port;
    public static Server IPv4Server = null;
    public static Server IPv6Server = null;

    public static bool IPV4Started = false;
    public static bool IPV6Started = false;
    private const uint ByteSize = 1024; // Data buffer size for incommming data
    private ServerType _serverType;

    public Server(uint Port)
    {
        //IpAdress = IP;
        _port = Port;
        _serverType = ServerType.Ipv4;
    }

    public Server(uint Port, ServerType ServerT)
    {
        //IpAdress = IP;
        _port = Port;
        _serverType = ServerT;
    }

    private IPAddress GetIPV4(IPAddress[] IPs)
    {
        foreach (IPAddress IP in IPs)
        {
            if (IPHandler.IsIPV4(IP))
            {
                return IP;
            }
        }
        return null;
    }

    private void PrintIps()
    {
        IPAddress[] IPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        foreach (IPAddress IP in IPs)
        {
            Console.WriteLine($"Listning on {IP.ToString()}:{_port}");
        }
    }

    public void StartListening()
    {
        // Delay for at sikre sig at IPv4 og IPv6 ikke starter samtidig
        Thread.Sleep(500);
        // IP resolve, finder selv IpAdresse for serveren.
        // Hoster på 0.0.0.0:?????
        // PrintIps();
        // IPV4 SERVER ->->->->->->->->->->->->->->->->->
        if (_serverType == ServerType.Ipv4)
        {
            Console.WriteLine("Type: IPv4");
            IPEndPoint localEndPointv4 = new IPEndPoint(IPAddress.Any, (int)_port);
            Socket listenerv4 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerv4.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPV4Started = true;
            IPv4Server = this;
            SocketServer(localEndPointv4, listenerv4);

        }
        // IPV6 SERVER ->->->->->->->->->->->->->->->->->
        else if (_serverType == ServerType.Ipv6)
        {
            Console.WriteLine("Type: IPv6");
            IPEndPoint localEndPointv6 = new IPEndPoint(IPAddress.IPv6Any,(int)_port);
            Socket listenerv6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            listenerv6.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPV6Started = true;
            IPv6Server = this;
            SocketServer(localEndPointv6, listenerv6);
        }
    }

    private bool IsObject(ref string Input)
    {
        if (Json.GetTypeFromString(Input) != null)
        {
            Input = Input.Replace("<EOF>", "");
            return true;
        }
        return false;
    }

    private void SocketServer(IPEndPoint localEndPoint, Socket listener)
    {
        string data;
        byte[] bytes = new Byte[ByteSize];

        listener.Bind(localEndPoint);
        listener.Listen(100);
        // Start listening for connections. 
        Console.WriteLine($"IP: {localEndPoint.Address}");
        Console.Write("Server Starting...");
        Print.PrintSuccessFailedLine(_serverType == ServerType.Ipv4 ?IPV4Started:IPV6Started);
        Print.PrintLine(ConsoleColor.Green);

        //Console.WriteLine($"{localEndPoint.Address.ToString()}:{localEndPoint.Port.ToString()}");
        while (true)
        {
            Stopwatch Ping = new Stopwatch();
            Stopwatch PingObject = new Stopwatch();
            Socket handler = listener.Accept();
            try
            {
                Ping.Restart();
                PingObject.Restart();
                // Program is suspended while waiting for an incoming connection.  
                data = null;
                // An incoming connection needs to be processed.  
                HandleConnection(handler, ref bytes, ref data);
                Console.WriteLine($"Incomming connection from {handler.RemoteEndPoint.ToString()}");
                //Console.WriteLine(data);
                Ping.Stop();
                if (IsObject(ref data))
                {
                    HandleObject(data);
                }
                PingObject.Stop();
                // Message to return to sender

                byte[] msg = Encoding.UTF8.GetBytes("1");
                handler.Send(msg);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }
            finally
            {

                Console.WriteLine($"Ping: {Ping.ElapsedMilliseconds} ms");
                Console.WriteLine($"Ping_Object: {PingObject.ElapsedMilliseconds} ms");

                
                // Shutdown somehow causes problems when running on linux...
                // handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }

    public static void StartServer(bool IPv4, bool IPv6)
    {
        // Hvis den skal starte IPV4 Server
        if (IPv4)
        {
            Server SocketServerIPv4 = new Server(12943, ServerType.Ipv4);
            Thread IPv4Thread = new Thread(new ThreadStart(SocketServerIPv4.StartListening));
            IPv4Thread.Start();
            Log.LogData("ServerStart", "Startede IPv4 Socket Server");
        }
        // Hvis den skal starte IPV6 Server
        if (IPv6)
        {
            // Hvis der også er bedt om at få startet en IPV4 server, så skal den vente på at den er startet først.
            if (IPv4)
            {
                Utilities.WaitFor(ref Server.IPV4Started);
            }
            Server SocketServerIPv6 = new Server(12943, ServerType.Ipv6);
            Thread IPv6Thread = new Thread(new ThreadStart(SocketServerIPv6.StartListening));
            IPv6Thread.Start();
            Log.LogData("ServerStart", "Startede IPv6 Socket Server");

        }

    }

    private void HandleConnection(Socket handler, ref byte[] bytes, ref string data)
    {
        bytes = new byte[ByteSize];
        int bytesRec = handler.Receive(bytes);
        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
        /*
        if (data.IndexOf("<EOF>") > -1)
        {
            break;
        }
        */
    }

    private void HandleObject(string Obj)
    {
        Type type = Json.GetTypeFromString(Obj);
        var obj = Json.Deserialize(Obj);
        obj.Start();
    }
}
