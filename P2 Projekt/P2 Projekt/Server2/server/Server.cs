using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Collections.Immutable;
using JsonSerializer;

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

    private const int NumberOfWorkers = 5;

    public static bool IPV4Started = false;

    public static bool IPV6Started = false;

    private const uint ByteSize = 1024; // Data buffer size for incommming data

    private const uint KBSize = 1024;

    private Thread WorkerDelagateThread;

    private ServerType _serverType;

    public List<Socket> ConnectionWaiting = new List<Socket> { };

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
            WorkerDelagateThread = new Thread(new ThreadStart(HandleWorkers));
            WorkerDelagateThread.Start();
            SocketServer(localEndPointv4, listenerv4);
        }
        // IPV6 SERVER ->->->->->->->->->->->->->->->->->
        else if (_serverType == ServerType.Ipv6)
        {
            Console.WriteLine("Type: IPv6");
            IPEndPoint localEndPointv6 = new IPEndPoint(IPAddress.IPv6Any, (int)_port);
            Socket listenerv6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            listenerv6.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPV6Started = true;
            IPv6Server = this;
            WorkerDelagateThread = new Thread(new ThreadStart(HandleWorkers));
            WorkerDelagateThread.Start();
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

    private bool IsRequest(ref string Input)
    {
        // Hvis det første ord er Request
        if (Input.Substring(0, Input.IndexOf(',')) == "request")
        {
            Input = Input.Replace("<EOF>", "");
            return true;
        }
        return false;
    }

    public ObjectTypes GetRequestType(ref string input)
    {
        // Strengen uden request
        input = input.Replace("request,", "");
        // Checkstring bliver lige simpel
        string checkString = input.Substring(0, input.IndexOf(","));
        // input = input.Substring(input.IndexOf(","),input.Length-input.IndexOf(",")); //--Not needed at this time
        if (checkString == "Bus")
        {
            return ObjectTypes.Bus;
        }
        else if (checkString == "BusStop")
        {
            return ObjectTypes.BusStop;
        }
        else
        {
            throw new UnknownObjectException("Ukendt object forventet");
        }
    }

    public string GetWhereCondition(ref string input)
    {
        // Retunere alt efter hvad der er valgt som object, så der kun er where condition tilbage
        //Stregen er : request,ALL,{OBJECT},{WHERE}
        string WhereCondition = input.Substring(input.IndexOf(',') + 1);

        return WhereCondition;
    }

    public string GetRequestParams(string input)
    {
        // Retunere enten et ID, eller ALL
        return input.Substring(input.IndexOf(","));
    }

    private string GenerateResponse(ObjectTypes ObjType, string WhereCondition, bool All)
    {
        TableDecode OutputObject;
        string OutputString;

        //Stregen er : request,ALL,{OBJECT},{WHERE}
        if (All)
        {
            switch (ObjType)
            {
                case ObjectTypes.Bus:
                    break;
                case ObjectTypes.BusStop:
                    Stoppested Stoppested = new Stoppested();
                    TableDecode RowsFromDB;
                    if (WhereCondition == "None")
                    {
                        RowsFromDB = MysqlControls.SelectAll(Stoppested.GetTableName());

                    }
                    else
                    {
                        RowsFromDB = MysqlControls.SelectAllWhere(Stoppested.GetTableName(), WhereCondition);
                    }
                    List<Stoppested> StoppeSteder = new List<Stoppested>();
                    foreach (var SS in RowsFromDB.RowData)
                    {
                        Stoppested NewStop = new Stoppested();
                        NewStop.Update(SS);
                        StoppeSteder.Add(NewStop);
                    }
                    OutputString = Json.Serialize(StoppeSteder);
                    return OutputString;
                case ObjectTypes.Unknown:
                    break;
                default:
                    break;
            }

        }
        // Hvis der kun er
        else
        {
            switch (ObjType)
            {
                case ObjectTypes.Bus:
                    Bus BusObject = new Bus();
                    OutputObject = BusObject.GetThisFromDB(WhereCondition);
                    BusObject.Update(OutputObject);
                    OutputString = Json.Serialize(BusObject);
                    return OutputString;
                case ObjectTypes.BusStop:
                    Stoppested Stoppested = new Stoppested();
                    TableDecode RowsFromDB = MysqlControls.SelectAllWhere(Stoppested.GetTableName(), WhereCondition);
                    List<Stoppested> StoppeSteder = new List<Stoppested>();
                    foreach (var SS in RowsFromDB.RowData)
                    {
                        Stoppested NewStop = new Stoppested();
                        NewStop.Update(SS);
                        StoppeSteder.Add(NewStop);
                    }
                    OutputString = Json.Serialize(StoppeSteder);
                    return OutputString;
                default:
                    throw new UnknownObjectException("Dette er et ukendt object");
            }
        }
        return "1";
    }

    public string CheckMessage(string data)
    {
        if (IsObject(ref data))
        {
            HandleObject(data);
        }
        else if (IsRequest(ref data))
        {
            data = data.Substring(data.IndexOf(',') + 1);
            bool All = CheckAll(ref data);

            ObjectTypes ObjType = GetRequestType(ref data);
            string WhereCondition = GetWhereCondition(ref data);
            string Response = GenerateResponse(ObjType, WhereCondition, All);
            return Response;
        }
        return "1";
    }

    private bool CheckAll(ref string data)
    {

        if (data.Substring(0, data.IndexOf(',')) == "ALL")
        {
            data = data.Substring(data.IndexOf(',') + 1);
            return true;
        }
        else
        {
            data = data.Substring(data.IndexOf(',') + 1);
            return false;
        }
    }

    private void SocketServer(IPEndPoint localEndPoint, Socket listener)
    {

        listener.Bind(localEndPoint);
        listener.Listen(100);
        // Start listening for connections. 
        Console.WriteLine($"IP: {localEndPoint.Address}");
        Console.Write("Server Starting...");
        Print.PrintSuccessFailedLine(_serverType == ServerType.Ipv4 ? IPV4Started : IPV6Started);
        Print.PrintLine(ConsoleColor.Green);
        // StartWorkers();
        //Console.WriteLine($"{localEndPoint.Address.ToString()}:{localEndPoint.Port.ToString()}");
        while (true)
        {
            Socket handler = listener.Accept();
            lock (ConnectionWaiting)
            {
                ConnectionWaiting.Add(handler);
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

    private long HandleConnection(Socket handler, ref byte[] bytes, ref string data)
    {
        data = "";
        int bytesRec = 0;
        List<byte> Bytes = new List<byte>(1024 * 4);
        do
        {
            bytes = new byte[1];
            bytesRec = handler.Receive(bytes);
            Bytes.Add(bytes[0]);
        } while (handler.Available > 0);
        data = Encoding.UTF8.GetString(Bytes.ToArray(), 0, Bytes.Count);
        if (data.IndexOf("<EOF>") == -1)
        {
            throw new NoEndOfFileFoundException();
        }
        return Bytes.Count;

    }

    private void HandleObject(string Obj)
    {

        Type type = Json.GetTypeFromString(Obj);
        List<NetworkObject> Objects = Json.Deserialize(Obj);
        Print.PrintCenterColor("Got: ", Objects.Count.ToString(), " Objects", ConsoleColor.Cyan);
        foreach (NetworkObject SingleObject in Objects)
        {
            Thread SingleObjectThread = new Thread(new ThreadStart(SingleObject.Start));
            SingleObjectThread.Start();
        }
    }

    private long PingRemote(EndPoint Remote)
    {
        long pingTime = 0;
        Ping pingSender = new Ping();
        PingReply reply = pingSender.Send(Remote.ToString().Substring(0, Remote.ToString().IndexOf(":")));
        pingTime = reply.RoundtripTime;
        return pingTime;
    }

    // --------------------------------------------------------------
    // ------------------------ THREAD WORK HERE --------------------
    // --------------------------------------------------------------

    private void HandleSocketConnectionThread(object Handler_pre)
    {
        if (Handler_pre == null)
        {
            Console.WriteLine("Der er ikke noget object?!");
        }
        Socket handler = Handler_pre as Socket;
        string data;
        string response;
        byte[] bytes = new byte[] { };
        long PingClient = 0;
        Stopwatch Ping = new Stopwatch();
        Stopwatch PingObject = new Stopwatch();
        Stopwatch PingTotal = new Stopwatch();
        try
        {
            Ping.Restart();
            PingObject.Restart();
            PingTotal.Restart();
            // Program is suspended while waiting for an incoming connection.  
            data = null;
            response = "1";
            // An incoming connection needs to be processed.  
            double SizeOfMsg = Math.Round((double)HandleConnection(handler, ref bytes, ref data) / 1024, 2); // Retunere hvor mange KB der er blevet modtaget
            PingClient = PingRemote(handler.RemoteEndPoint);
            Console.Write($"Incomming connection from ");
            int NonNullElements = ArrayHandler.CountNonZeroElementsInByteArray(bytes);
            Print.PrintColorLine(handler.RemoteEndPoint.ToString(), ConsoleColor.Yellow);
            Console.Write($"Size: ");
            Print.PrintColor(SizeOfMsg.ToString(), ConsoleColor.Green);
            Console.WriteLine(" KB");
            //Console.WriteLine(data);
            Ping.Stop();
            // Checker om beskeden der er modtaget, indeholder noget data som skal bruges. 
            response = CheckMessage(data);
            response += "<EOF>";
            PingObject.Stop();
            // Tester om objectet der skal retuneres kan deserailiseres...
            // Laver Response om fra en string til bytes baseret på UTF8
            byte[] msg = Encoding.UTF8.GetBytes(response);
            // Sender beskeden. 
            handler.Send(msg);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            PingTotal.Stop();
            // handler.Shutdown(SocketShutdown.Both); //--Skaber problemer på Linux
            handler.Close();
            Print.PrintColorLine($"Ping: {PingClient} ms | {Ping.ElapsedMilliseconds} ms | {PingObject.ElapsedMilliseconds} ms | {PingTotal.ElapsedMilliseconds} ms", ConsoleColor.Yellow);
        }
    }

    private void HandleWorkers()
    {
        while (true)
        {
            if (ConnectionWaiting.Count > 0)
            {
                Thread NewThread = new Thread(new ParameterizedThreadStart(HandleSocketConnectionThread));
                Socket handler = ConnectionWaiting[0];
                NewThread.Start(handler);
                lock (ConnectionWaiting)
                {
                    ConnectionWaiting.Remove(ConnectionWaiting[0]);
                }
            }
        }
    }
}