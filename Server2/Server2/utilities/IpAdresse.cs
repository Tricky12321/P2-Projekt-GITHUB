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
}
