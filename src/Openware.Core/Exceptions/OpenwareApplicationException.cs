namespace Openware.Core;
public class OpenwareApplicationException : Exception
{
    public OpenwareApplicationException() : base(Resources.ApplicationFailure)
    {
    }

    public OpenwareApplicationException(string message) : base(message)
    {
    }
}
