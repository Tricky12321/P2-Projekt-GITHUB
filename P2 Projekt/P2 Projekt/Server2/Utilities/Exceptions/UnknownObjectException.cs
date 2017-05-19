using System;
public class UnknownObjectException : Exception
{
    public UnknownObjectException()
    {

    }

    public UnknownObjectException(string message) : base(message)
    {

    }

    public UnknownObjectException(string message, Exception inner) : base(message, inner)
    {

    }
}