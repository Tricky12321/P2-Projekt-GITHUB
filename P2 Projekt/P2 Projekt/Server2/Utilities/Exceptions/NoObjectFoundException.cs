using System;
public class NoObjectFoundException : Exception
{
    public NoObjectFoundException()
    {

    }

    public NoObjectFoundException(string message) : base(message)
    {

    }

    public NoObjectFoundException(string message, Exception inner) : base(message, inner)
    {

    }
}