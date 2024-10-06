namespace Openware.Core;
public class NotAuthorizedAccessException:Exception
{
    public NotAuthorizedAccessException() : base(Resources.Unauthorized)
    {
    }

    public NotAuthorizedAccessException(string message) : base(message)
    {
    }
}
