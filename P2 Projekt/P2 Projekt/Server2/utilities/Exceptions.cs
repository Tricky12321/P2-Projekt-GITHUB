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