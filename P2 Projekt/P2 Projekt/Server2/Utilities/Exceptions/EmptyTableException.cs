using System;
public class EmptyTableException : Exception
{

    public EmptyTableException()
    {

    }

    public EmptyTableException(string message) : base(message)
    {

    }

    public EmptyTableException(string message, Exception inner) : base(message, inner)
    {

    }
}