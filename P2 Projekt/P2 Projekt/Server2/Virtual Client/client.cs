using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
    private string _host = "127.0.0.1";
    //private string _host = "192.168.84.124";
    private uint _port = Server.IPv4Server.GetPort;
    private string LongString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce sit amet pretium ex, id hendrerit tortor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi pulvinar, sem vitae porttitor lacinia, est lorem egestas sem, ac elementum urna quam sed lacus. Phasellus sagittis euismod velit eu eleifend. Pellentesque et vulputate nibh. Sed mi ante, vestibulum nec lobortis vitae, suscipit quis libero. Integer vitae ultricies augue. Morbi mauris urna, tristique sit amet molestie ut, interdum ut magna. Vestibulum ullamcorper interdum consequat. Vestibulum vitae ante ut nunc scelerisque semper eu eget eros. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc ultricies massa in fermentum feugiat. Proin a placerat risus. In congue lacus nibh, ut pretium ante dapibus ac. Suspendisse finibus nunc ut tempus aliquam. Cras in nibh hendrerit, suscipit nisl ac, placerat massa. Vestibulum fringilla accumsan orci. Sed nec dolor vitae orci pharetra auctor vitae nec purus. Praesent luctus erat at commodo fermentum. Vivamus nec aliquet turpis, eu molestie elit. In auctor, odio vitae dictum tempus, dui lorem hendrerit tellus, ac pellentesque mauris nisl vitae lacus. Nullam non tempor ex, mollis lacinia ex. Vivamus pellentesque ex ac mi sodales congue. Integer vel leo a augue scelerisqu interdum. Integer volutpat mollis felis ac iaculis. Sed rhoncus turpis at elit pellentesque fermentum.";
    public void SendTestObject()
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
            sender = new Socket(IPHandler.IsIPV6(ipAddress) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create a TCP/IP  socket.  
            // Connect the socket to the remote endpoint. Catch any errors.

            try
            {
                sender.Connect(remoteEP);
                // Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  
                //Console.WriteLine(LongString + LongString + LongString + LongString + LongString);
                string JsonString = Json.Serialize(new TestObject(123, "ASDF", "QWÅØXZYABC", ";:_,.-*@^~?=!#/€$£@€\\[]{}()", "Ð ¼ ½ ¾ » ¶ µ ± ® ©", LongString)) + "<EOF>";
                byte[] msg = Encoding.UTF8.GetBytes(JsonString);

                /*
                if (msg.Length > BytesToSend)
                {
                    throw new TooManyBytesException("Der blev sendt for meget data");
                }
                */
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
        // return output;
    }
}

