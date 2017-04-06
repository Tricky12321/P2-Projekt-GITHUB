using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Server
{
    private uint _port;

    private const uint ByteSize = 1024; // Data buffer size for incommming data

    public Server(uint Port)
    {
        //IpAdress = IP;
        _port = Port;
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
        // IP resolve, finder selv IpAdresse for serveren.
        // Hoster på 0.0.0.0:_port
        // PrintIps();
        IPEndPoint localEndPoint = new IPEndPoint(0, (int)_port);
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        SocketServer(localEndPoint, listener);
        // Create a TCP/IP socket.  

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
        Console.WriteLine("Server started!");
        //Console.WriteLine($"{localEndPoint.Address.ToString()}:{localEndPoint.Port.ToString()}");
        while (true)
        {
            Socket handler = listener.Accept();
            try
            {
                // Program is suspended while waiting for an incoming connection.  
                data = null;
                // An incoming connection needs to be processed.  
                HandleConnection(handler, ref bytes, ref data);
                Console.WriteLine($"Incomming connection from {handler.RemoteEndPoint.ToString()}");
                Console.WriteLine(data);
                if (IsObject(ref data))
                {
                    HandleObject(data);
                }
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
                // handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
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
        // dynamic v2 = type.GetType().GetProperty("Value").GetValue(type, null);
        dynamic v3 = Activator.CreateInstance(type);
        v3 = Json.Deserialize(Obj);
        (v3 as NetworkObject).Start();
    }
}
