using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using JsonSerializer;


public class Server
{
    private uint _connectionsWaiting = 0;
    private uint _port;

    public uint GetPort => _port;

    public static Server IPv4Server = null;

    public static Server IPv6Server = null;

    // private const int NumberOfWorkers = 5;

    public static bool IPV4Started = false;

    public static bool IPV6Started = false;

    // private const uint ByteSize = 1024; // Data buffer size for incommming data

    // private const uint KBSize = 1024;

    private Thread WorkerDelagateThread;

    private ServerType _serverType;

    public List<Socket> ConnectionWaiting = new List<Socket> { };

    public Server(uint Port, ServerType ServerT)
    {
        //IpAdress = IP;
        _port = Port;
        _serverType = ServerT;
    }

    public void StartListening()
    {
        // Delay for at sikre sig at IPv4 og IPv6 ikke starter samtidig
        //Thread.Sleep(500);
        // IP resolve, finder selv IpAdresse for serveren.
        // Hoster på 0.0.0.0:?????
        // PrintIps();
        // IPV4 SERVER ->->->->->->->->->->->->->->->->->
        if (_serverType == ServerType.Ipv4)
        {
            Print.WriteLine("Type: IPv4");
            IPEndPoint localEndPointv4 = new IPEndPoint(IPAddress.Any, (int)_port);
            Socket listenerv4 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerv4.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPV4Started = true;
            IPv4Server = this;
            (WorkerDelagateThread = new Thread(new ThreadStart(HandleWorkers))).Start();
            SocketServer(localEndPointv4, listenerv4);
        }
        // IPV6 SERVER ->->->->->->->->->->->->->->->->->
        else if (_serverType == ServerType.Ipv6)
        {
            Print.WriteLine("Type: IPv6");
            IPEndPoint localEndPointv6 = new IPEndPoint(IPAddress.IPv6Any, (int)_port);
            Socket listenerv6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            listenerv6.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPV6Started = true;
            IPv6Server = this;
            (WorkerDelagateThread = new Thread(new ThreadStart(HandleWorkers))).Start();
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
        else if (checkString == "Rute")
        {
            return ObjectTypes.Rute;
        }
        else if (checkString == "AfPaaTidCombi")
        {
            return ObjectTypes.AfPaaTidCombi;
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
        return input.Substring(input.IndexOf(',') + 1);
    }


    private string GenerateResponse(ObjectTypes ObjType, string WhereCondition, bool All)
    {
        //Stregen er : request,ALL,{OBJECT},{WHERE}
        if (All)
        {
            switch (ObjType)
            {
                case ObjectTypes.Bus:
                    return HandleBus(WhereCondition);
                case ObjectTypes.Rute:
                    return HandleRute(WhereCondition);
                case ObjectTypes.BusStop:
                    return HandleBusStop(WhereCondition);
                case ObjectTypes.AfPaaTidCombi:
                    return HandleAfPåTidCombi(WhereCondition);
                case ObjectTypes.Unknown:
                    break;
            }
        }
        return "1";
    }

    private string HandleBus(string WhereCondition)
    {
        string OutputString;
        TableDecode RowsFromDB;
        Bus SingleBus = new Bus();
        if (WhereCondition == "None")
        {
            if (JsonCache.AlleBusserCache != null)
            {
                return JsonCache.AlleBusserCache;
            }
            RowsFromDB = MysqlControls.SelectAll(SingleBus.GetTableName());
        }
        else
        {
            RowsFromDB = MysqlControls.SelectAllWhere(SingleBus.GetTableName(), WhereCondition);
        }
        List<Bus> AlleBusser = new List<Bus>();
        foreach (var SS in RowsFromDB.RowData)
        {
            Bus NewBus = new Bus();
            NewBus.Update(SS);
            AlleBusser.Add(NewBus);
        }
        OutputString = Json.Serialize(AlleBusser);
        return OutputString;
    }

    private string HandleRute(string WhereCondition)
    {
        string OutputString;
        TableDecode RowsFromDB;
        Rute SingleRute = new Rute();
        if (WhereCondition == "None")
        {
            if (JsonCache.AlleRuterCache != null)
            {
                return JsonCache.AlleRuterCache;
            }
            RowsFromDB = MysqlControls.SelectAll(SingleRute.GetTableName());

        }
        else
        {
            RowsFromDB = MysqlControls.SelectAllWhere(SingleRute.GetTableName(), WhereCondition);
        }
        List<Rute> AlleRuter = new List<Rute>();
        foreach (var SS in RowsFromDB.RowData)
        {
            Rute NewRute = new Rute();
            NewRute.Update(SS);
            AlleRuter.Add(NewRute);
        }
        OutputString = Json.Serialize(AlleRuter);
        return OutputString;
    }

    private string HandleBusStop(string WhereCondition)
    {
        string OutputString;
        TableDecode RowsFromDB;
        Stoppested Stoppested = new Stoppested();
        if (WhereCondition == "None")
        {
            if (JsonCache.AlleStoppeStederCache != null)
            {
                return JsonCache.AlleStoppeStederCache;
            }
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
    }

    private string HandleAfPåTidCombi(string WhereCondition)
    {
        string OutputString;
        TableDecode RowsFromDB;
        AfPåTidCombi afPaa = new AfPåTidCombi();
        if (WhereCondition == "None")
        {
            RowsFromDB = MysqlControls.SelectAll(afPaa.GetTableName());
        }
        else
        {
            RowsFromDB = MysqlControls.SelectAllWhere(afPaa.GetTableName(), WhereCondition);
        }
        List<AfPåTidCombi> afPaaList = new List<AfPåTidCombi>();
        foreach (var SS in RowsFromDB.RowData)
        {
            AfPåTidCombi newAfPaa = new AfPåTidCombi();
            newAfPaa.Update(SS);
            afPaaList.Add(newAfPaa);
        }
        OutputString = Json.Serialize(afPaaList);
        return OutputString;
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
            return GenerateResponse(ObjType, WhereCondition, All);
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
        listener.Listen(100); // hvor mange forbindelser der kan være i kø ad gangen. 
        // Start listening for connections. 
        Print.WriteLine($"IP: {localEndPoint.Address}");
        Print.Write("Server Starting...");
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
                _connectionsWaiting++;
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
            if (SingleObject is MysqlObject)
            {
                (SingleObject as MysqlObject).UploadToDatabase();
            }
            else
            {
                SingleObjectThread.Start();

            }
        }
    }

    // --------------------------------------------------------------
    // ------------------------ THREAD WORK HERE --------------------
    // --------------------------------------------------------------

    private void HandleSocketConnectionThread(object Handler_pre)
    {
        if (Handler_pre == null)
        {
            Print.WriteLine("Der er ikke noget object?!");
        }
        else
        {
            Socket handler = Handler_pre as Socket;
            string data = null;
            string response;
            byte[] bytes = new byte[] { };
            try
            {
                response = "1";
                double SizeOfMsgRec = Math.Round((double)HandleConnection(handler, ref bytes, ref data) / 1024, 2); // Retunere hvor mange KB der er blevet modtaget
                                                                                                                    // Checker om beskeden der er modtaget, indeholder noget data som skal bruges. 
                response = CheckMessage(data);
                response += "<EOF>";
                // Tester om objectet der skal retuneres kan deserailiseres...
                // Laver Response om fra en string til bytes baseret på UTF8
                byte[] msg = Encoding.UTF8.GetBytes(response);
                double SizeOfMsgSent = Math.Round((double)Encoding.UTF8.GetByteCount(response) / 1024, 2);
                Print.PrintCenterColorSingle("Connection: ", handler.RemoteEndPoint.ToString().PadRight(25), " | ", ConsoleColor.Yellow);
                Print.PrintCenterColorSingle("R: ", SizeOfMsgRec.ToString().PadRight(5), (" KB | ").PadRight(8), ConsoleColor.Green);
                Print.PrintCenterColorSingle("S: ", SizeOfMsgSent.ToString().PadRight(5), (" KB").PadRight(8) + "\n", ConsoleColor.Green);
                // Sender beskeden. 
                handler.Send(msg);
            }
            catch (Exception e)
            {
                Print.WriteLine(e.ToString());
            }
            finally
            {
                handler.Close();
            }
        }

    }

    private void HandleWorkers()
    {
        while (true)
        {

            if (_connectionsWaiting > 0)
            {
                if (ConnectionWaiting[0] != null)
                {
                    Thread NewThread = new Thread(new ParameterizedThreadStart(HandleSocketConnectionThread));
                    Socket handler = ConnectionWaiting[0];
                    NewThread.Start(handler);
                    lock (ConnectionWaiting)
                    {
                        ConnectionWaiting.Remove(ConnectionWaiting[0]);
                    }
                    _connectionsWaiting--;
                }
            }
            Thread.Sleep(10);
        }
    }
}