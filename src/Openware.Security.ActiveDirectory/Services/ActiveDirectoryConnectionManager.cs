using System.DirectoryServices.Protocols;
using System.Net;

using Microsoft.Extensions.Options;

using Openware.Core.Helpers;

namespace Openware.Security.ActiveDirectory;

/// <summary>
/// Default Implementation for <see cref="IActiveDirectoryConnectionManager"/>
/// </summary>
public class ActiveDirectoryConnectionManager : IActiveDirectoryConnectionManager
{
    private readonly IOptions<ActiveDirectoryConnectionOptions> _options;


    /// <summary>
    /// Creates a new instance of <see cref="ActiveDirectoryConnectionManager"></see>
    /// </summary>
    /// <param name="options">The options to use as defaults</param>
    public ActiveDirectoryConnectionManager(IOptions<ActiveDirectoryConnectionOptions> options)
        => _options = options;
    /// <summary>
    /// Opens a connection to Active Directory using the default options
    /// </summary>
    /// <returns></returns>
    public LdapConnection OpenConnection()
    {
        if (String.IsNullOrEmpty(_options.Value.DomainName))
        {
            throw new ArgumentNullException(nameof(_options.Value.DomainName));
        }

        if (String.IsNullOrEmpty(_options.Value.UserName))
        {
            throw new ArgumentNullException(nameof(_options.Value.UserName));
        }

        if (String.IsNullOrEmpty(_options.Value.Password))
        {
            throw new ArgumentNullException(nameof(_options.Value.Password));
        }

        return OpenConnection(_options.Value.DomainName, _options.Value.UserName, _options.Value.Password);
    }


    /// <summary>
    /// Opens a connection to Active Directory 
    /// </summary>
    /// <param name="domain">The domain name</param>
    /// <param name="userName">The user Name</param>
    /// <param name="password">The password</param>
    /// <returns>An instance of <see cref="LdapConnection"/></returns>
    public LdapConnection OpenConnection(string domain, string userName, string password)
    {
        ExceptionHelper.ThrowIfNullOrEmpty(domain);
        ExceptionHelper.ThrowIfNullOrEmpty(userName);
        ExceptionHelper.ThrowIfNullOrEmpty(password);
       
        var credentials = new NetworkCredential(userName, password);

        LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(domain);
        LdapConnection connection = new LdapConnection(identifier, credentials)
        {
            AuthType = AuthType.Ntlm

        };
        
        connection.SessionOptions.ProtocolVersion = 3;

        connection.SessionOptions.VerifyServerCertificate += (connection, certificate) => true;

        connection.Bind();

        return connection;
    }
}
