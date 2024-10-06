namespace Openware.Security.ActiveDirectory;

/// <summary>
/// The user details
/// </summary>
public class AdUser
{
    /// <summary>
    /// The user name
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// The email of the user
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// The display name of a user
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// The telpehone number
    /// </summary>
    public string TelephoneNumber { get; set; }
    /// <summary>
    /// if[true] then user is enabled
    /// </summary>
    public bool IsEnabled { get; set; }

    public override string ToString()
    {
        return $"UserName:{UserName ?? ""}, Email:{Email ?? ""}, Name:{DisplayName ?? ""}, " +
            $"Phone:{TelephoneNumber ?? ""}, IsEnabled:{IsEnabled}";
    }
}
