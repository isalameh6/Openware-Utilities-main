namespace Openware.Security.ActiveDirectory;

/// <summary>
/// A type which provides details regarding Active Directory Users
/// </summary>
public interface IActiveDirectoryUserManager
{

    /// <summary>
    /// Searches for an instance of a user by connecting with the provided arguments to Active directory
    /// </summary>
    /// <param name="userName">The user name</param>
    /// <returns>An Active Directory User <see cref="AdUser"/></returns>
    AdUser FindUserByNameOnly(string userName);

    /// <summary>
    /// Searches for an instance of a user by connecting with the provided arguments to Active directory
    /// </summary>
    /// <param name="domain">The domain name</param>
    /// <returns>An Active Directory User <see cref="AdUser"/></returns>
    AdUser FindUserByNameOnly(string domain,string userName);

    /// <summary>
    /// Searches for an instance of a user by connecting with the provided arguments to Active directory
    /// </summary>
    /// <param name="domain">The domain name</param>
    /// <param name="userName">The user name</param>
    /// <param name="password">The password</param>
    /// <returns>An Active Directory User <see cref="AdUser"/></returns>
    AdUser FindUser(string domain, string userName, string password);

    /// <summary>
    /// Searches for an instanc of a user by connecting with the provided arguments to Active directory, 
    /// </summary>
    /// <param name="userName">The user name</param>
    /// <param name="password">The password</param>
    /// <returns>An Active Directory User <see cref="AdUser"/></returns>
    AdUser FindUser(string userName, string password);

}