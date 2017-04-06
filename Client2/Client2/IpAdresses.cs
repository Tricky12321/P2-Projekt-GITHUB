using System.Net;

public static class IPHandler
{
    public static bool IsIPV4(IPAddress IP)
    {
        return (IP.ToString().Contains(".") && !IP.ToString().Contains(":"));
    }

    public static bool IsIPV6(IPAddress IP)
    {
        return (IP.ToString().Contains(":") && !IP.ToString().Contains("."));
    }

    public static IPAddress GetIpV4(IPAddress[] IPs)
    {
        foreach (IPAddress IP in IPs)
        {
            if (IsIPV4(IP))
            {
                return IP;
            }
        }
        return null;
    }

    public static IPAddress GetIpV6(IPAddress[] IPs)
    {
        foreach (IPAddress IP in IPs)
        {
            if (IsIPV6(IP))
            {
                return IP;
            }
        }
        return null;
    }
}
