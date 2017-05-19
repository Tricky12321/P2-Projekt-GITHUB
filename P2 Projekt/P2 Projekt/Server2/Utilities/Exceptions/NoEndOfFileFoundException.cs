using System;
public class NoEndOfFileFoundException : Exception
{
    public NoEndOfFileFoundException()
    {

    }

    public NoEndOfFileFoundException(string message) : base(message)
    {

    }

    public NoEndOfFileFoundException(string message, Exception inner) : base(message, inner)
    {

    }
}