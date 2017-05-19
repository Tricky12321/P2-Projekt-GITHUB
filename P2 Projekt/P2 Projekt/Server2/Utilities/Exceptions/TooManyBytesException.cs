using System;
public class TooManyBytesException : Exception
{
    public TooManyBytesException()
    {

    }

    public TooManyBytesException(string message) : base(message)
    {

    }

    public TooManyBytesException(string message, Exception inner) : base(message, inner)
    {

    }
}