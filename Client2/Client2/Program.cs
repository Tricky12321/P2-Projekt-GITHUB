using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Person : NetworkObject
{
    public string Name;
    public string Efternavn;

    public void Start()
    {

    }
}

public class Car : NetworkObject
{
    public Car(string _brand, bool _benzin, uint _passangers)
    {
        Brand = _brand;
        Benzin = _benzin;
        Diezel = !_benzin;
        Passangers = _passangers;
    }
    public string Brand;
    public bool Benzin;
    public bool Diezel;
    public uint Passangers;

    public void Start()
    {

    }
}

public class Garage : NetworkObject
{
    public List<Car> asdf = new List<Car>() { };
    public Garage()
    {
        Car Car1 = new Car("Toyota", false, 4);
        Car Car2 = new Car("Kia", true, 6);
        Car Car3 = new Car("Jeep", false, 10);
        Car Car4 = new Car("Bus", true, 52);
        Car Car5 = new Car("Lastbil", true, 2);
        asdf.Add(Car1);
        asdf.Add(Car2);
        asdf.Add(Car3);
        asdf.Add(Car4);
        asdf.Add(Car5);
    }

    public void Start()
    {

    }
}

public static class IPHandle
{
    public static IPAddress ResolveIpV4(string IP)
    {
        int intAddress = BitConverter.ToInt32(IPAddress.Parse(IP).GetAddressBytes(), 0);
        string ipAddress = new IPAddress(BitConverter.GetBytes(intAddress)).ToString();
        return new IPAddress(BitConverter.GetBytes(intAddress));
    }
}

public class SynchronousSocketClient
{
    public const int BytesToSend = 1024;
    public static string SerializePerson(Person person)
    {
        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(person.GetType());
        using (StringWriter textWriter = new StringWriter())
        {
            x.Serialize(textWriter, person);
            return "object\n" + textWriter.ToString();
        }
    }

    public static string Host;
    public const int Port = 12943;

    public static string SendCommand(string command)
    {

        // Data buffer for incoming data.  
        byte[] bytes = new byte[BytesToSend];
        string output = "No response";
        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // IPHostEntry ipHostInfo = Dns.GetHostEntry()
            // IPHostEntry IpTest = Dns.GetHostEntry(Host);
            IPAddress[] IPs = Dns.GetHostEntry(Host).AddressList;
            IPAddress ipAddress = IPHandler.GetIpV4(IPs);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

            Socket sender;
            sender = new Socket(IPHandler.IsIPV6(ipAddress) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Create a TCP/IP  socket.  
            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);
                // Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  

                byte[] msg = Encoding.UTF8.GetBytes(command + "<EOF>");
                if (msg.Length > BytesToSend)
                {
                    throw new TooManyBytesException();
                }
                Console.WriteLine($"Sent: {command}");
                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                output = Encoding.ASCII.GetString(bytes, 0, bytesRec);
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

    public static int Main(String[] args)
    {
        Console.WriteLine("IP:");
        Host = Console.ReadLine();
        while (true)
        {
            Console.ReadKey();
            Person p = new Person();
            p.Name = "Thue";
            p.Efternavn = "Iversen";
            Garage Gar = new Garage();
            SendCommand(Json.Serialize(Gar));

            //Console.WriteLine(SendCommand(Console.ReadLine()));
        }
        // return 0;
    }
}

public class TooManyBytesException : Exception
{

}

public interface NetworkObject
{
    void Start();
}