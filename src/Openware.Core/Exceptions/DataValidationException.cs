namespace Openware.Core;
public class DataValidationException : Exception
{
    public DataValidationException() : base(Resources.ValidationFailure)
    {
        Errors = new Dictionary<string, string[]>();
    }
    public DataValidationException(Dictionary<string, string[]> errors) : this()
    {
        foreach(var error in errors)
        {
            Errors.Add(error.Key, error.Value);
        }
    }
    public DataValidationException(String key, string message) : this()
    {
        Errors[key] = new string[] { message };
    }

    public IDictionary<string, string[]> Errors { get; }
}
