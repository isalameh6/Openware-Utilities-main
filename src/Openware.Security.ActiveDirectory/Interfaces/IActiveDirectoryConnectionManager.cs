using System.DirectoryServices.Protocols;

namespace Openware.Security.ActiveDirectory;

/// <summary>
/// A type which provides connections to LDAP
/// </summary>
public interface IActiveDirectoryConnectionManager
{
    /// <summary>
    /// Opens the connection using default options
    /// </summary>
    /// <returns>An <see cref="LdapConnection"/> instance</returns>
    LdapConnection OpenConnection();
    /// <summary>
    /// Opens the connection using provided options
    /// </summary>
    /// <param name="domain">The domain name</param>
    /// <param name="userName">The user name</param>
    /// <param name="password">The password</param>
    /// <returns>An <see cref="LdapConnection"/> instance</returns>
    LdapConnection OpenConnection(string domain, string userName, string password);
}