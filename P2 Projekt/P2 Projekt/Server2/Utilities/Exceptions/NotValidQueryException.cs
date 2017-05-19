using System;

public class NotValidQueryException : Exception
{
    public NotValidQueryException()
    {

    }

    public NotValidQueryException(string message) : base(message)
    {

    }

    public NotValidQueryException(string message, Exception inner) : base(message, inner)
    {

    }
}
