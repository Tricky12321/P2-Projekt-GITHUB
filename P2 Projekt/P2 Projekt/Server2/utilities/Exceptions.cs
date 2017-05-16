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

public class InvalidQueryException : Exception
{

    public InvalidQueryException()
    {

    }

    public InvalidQueryException(string message) : base(message)
    {

    }

    public InvalidQueryException(string message, Exception inner) : base(message, inner)
    {

    }
}

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

public class ConnectionFailedException : Exception
{
    public ConnectionFailedException()
    {

    }

    public ConnectionFailedException(string message) : base(message)
    {

    }

    public ConnectionFailedException(string message, Exception inner) : base(message, inner)
    {

    }
}

public class NotConnectedException : Exception
{
    public NotConnectedException()
    {

    }

    public NotConnectedException(string message) : base(message)
    {

    }

    public NotConnectedException(string message, Exception inner) : base(message, inner)
    {

    }
}

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