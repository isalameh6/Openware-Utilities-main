namespace Openware.Security.ActiveDirectory;

/// <summary>
/// A helper class for active directory data
/// </summary>
internal static class ActiveDirectoryUtils
{
    /// <summary>
    /// Returns the text in format of dc=[value]
    /// </summary>
    /// <param name="domainName">The domain name</param>
    /// <returns></returns>
    public static string GetDomainControllerNames(string domainName)
    {
        return String.IsNullOrEmpty(domainName) ? null : String.Join(",", domainName.Split(".").Select(x => "dc=" + x));
    }
}
