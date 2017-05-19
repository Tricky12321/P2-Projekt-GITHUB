using System;
public class NotCorrectObject : Exception
{
    public NotCorrectObject()
    {

    }

    public NotCorrectObject(string message) : base(message)
    {

    }

    public NotCorrectObject(string message, Exception inner) : base(message, inner)
    {

    }
}