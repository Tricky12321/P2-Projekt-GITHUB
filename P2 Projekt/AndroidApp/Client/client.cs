using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections.Generic;

public static class IPHandle
{
    public static IPAddress ResolveIpV4(string IP)
    {
        int intAddress = BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes(), 0);
        string ipAddress = new IPAddress(BitConverter.GetBytes(intAddress)).ToString();
        return new IPAddress(BitConverter.GetBytes(intAddress));
    }
}

public class Client
{
    private const int BytesToSend = 1024;
    //private string _host = "192.168.84.124";  // Router
    private string _host = "172.25.11.120";     // AAU
    //private string _host = "192.168.0.102";   // Lokal
    //private string _host = "172.24.18.136";

    private uint _port = 12943;
    public string LocalEndPoint;
    public string GetHost()
    {
        return _host;
    }

    public string SendTestObject()
    {

        // Data buffer for incoming data.  
        byte[] bytes = new byte[BytesToSend];
        string output = "No response";
        // Connect to a remote device.  
        try
        {
            IPAddress[] IPs = Dns.GetHostEntry(_host).AddressList;
            IPAddress ipAddress = IPHandler.GetIpV4(IPs);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, (int)_port);

            Socket sender;
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create a TCP/IP  socket.  
            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);
                // Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
                LocalEndPoint = sender.LocalEndPoint.ToString();
                // Encode the data string into a byte array.  
                byte[] msg = Encoding.UTF8.GetBytes(Json.Serialize(new TestObject()) + "<EOF>");
                if (msg.Length > BytesToSend)
                {
                    throw new TooManyBytesException();
                }
                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);
                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                output = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                //Print.PrintColorLine(output, ConsoleColor.Cyan);
                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return output;
    }
}

