﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using JsonSerializer;
public class RealClient
{
    //private string _host = "172.25.11.120";
    private string _host = "127.0.0.1";
    private uint _port = 12943;

    public void SendObject(object ObjToSend, Type TypeOfObj)
    {
        // Data buffer for incoming data.  
        byte[] bytes = new byte[] { };
        string output = "No response";
        // Connect to a remote device.  
        try
        {
            IPAddress[] IPs = Dns.GetHostEntry(_host).AddressList;
            IPAddress ipAddress = IPHandler.GetIpV4(IPs);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, (int)_port);

            Socket sender;
            sender = new Socket(IPHandler.IsIPV6(ipAddress.ToString()) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create a TCP/IP  socket.  
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);
                // Encode the data string into a byte array.  
                string JsonString = Json.Serialize(ObjToSend) + "<EOF>";
                byte[] msg = Encoding.UTF8.GetBytes(JsonString);
                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);
                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                output = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Print.PrintColorLine(output, ConsoleColor.Cyan);
                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Print.WriteLine($"ArgumentNullException : {ane.ToString()}");
            }
            catch (SocketException se)
            {
                Print.WriteLine($"SocketException : {se.ToString()}");
            }
            catch (Exception e)
            {
                Print.WriteLine($"Unexpected exception : {e.ToString()}");
            }
        }
        catch (Exception e)
        {
            Print.WriteLine(e.ToString());
        }
    }

    public List<NetworkObject> RequestAllWhere(ObjectTypes ObjType, string WhereCondition)
    {
        //Stregen er : request,{OBJECT},{WHERE}
        if (WhereCondition == "")
        {
            WhereCondition = "None";
        }
        string RequestString = $"request,ALL,{ObjType.ToString()},{WhereCondition}";
        // Data buffer for incoming data.  
        byte[] bytes = new byte[] { };
        string ReturnString = "No response";
        List<NetworkObject> ReturnList = new List<NetworkObject>();
        // Connect to a remote device.  
        try
        {
            IPAddress[] IPs = Dns.GetHostEntry(_host).AddressList;
            IPAddress ipAddress = IPHandler.GetIpV4(IPs);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, (int)_port);

            Socket sender;
            sender = new Socket(IPHandler.IsIPV6(ipAddress.ToString()) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create a TCP/IP  socket.  
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);
                // Encode the data string into a byte array.  
                string RequestStringFinal = RequestString + "<EOF>";
                System.Diagnostics.Debug.Print($"Requestion ALL: {RequestString}");
                byte[] msg = Encoding.UTF8.GetBytes(RequestStringFinal);
                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);
                // Receive the response from the remote device. 
                long bytesRec = HandleConnection(sender, ref bytes, ref ReturnString);
                if (ReturnString != "1<EOF>")
                {
                    ReturnList = Json.Deserialize(ReturnString);
                }
                else
                {
                    System.Diagnostics.Debug.Print("Der er ikke noget at deseralisere");

                }
                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (ArgumentNullException ane)
            {
                Print.WriteLine($"ArgumentNullException : {ane.ToString()}");
            }
            catch (SocketException se)
            {
                Print.WriteLine($"SocketException : {se.ToString()}");
            }
            catch (Exception e)
            {
                Print.WriteLine($"Unexpected exception : {e.ToString()}");
            }
        }
        catch (Exception e)
        {
            Print.WriteLine(e.ToString());
        }
        return ReturnList;
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
        // Tjekker om alt data er modtaget
        if (data.IndexOf("<EOF>") == -1)
        {
            throw new NoEndOfFileFoundException();
        }
        return Bytes.Count;

    }
}

