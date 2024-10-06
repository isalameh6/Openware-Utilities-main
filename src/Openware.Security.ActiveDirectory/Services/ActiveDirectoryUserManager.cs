
using System.DirectoryServices.Protocols;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Openware.Core.Helpers;

namespace Openware.Security.ActiveDirectory;

/// <summary>
/// The default implementation for <see cref="IActiveDirectoryUserManager"/>
/// </summary>
public class ActiveDirectoryUserManager : IActiveDirectoryUserManager
{
    private readonly IActiveDirectoryConnectionManager _activeDirectoryManager;
    private readonly IOptions<ActiveDirectoryConnectionOptions> _options;
    private readonly ILogger<ActiveDirectoryConnectionManager> _logger;

    /// <summary>
    /// Creates a new instance of <see cref="ActiveDirectoryUserManager"/>
    /// </summary>
    /// <param name="activeDirectoryManager">The acitve directory manager</param>
    /// <param name="options">The connection options</param>
    /// <param name="logger">The logger instance</param>
    public ActiveDirectoryUserManager(IActiveDirectoryConnectionManager activeDirectoryManager, IOptions<ActiveDirectoryConnectionOptions> options, ILogger<ActiveDirectoryConnectionManager> logger)
    {
        _activeDirectoryManager = activeDirectoryManager;
        _options = options;
        _logger = logger;
    }

    public AdUser FindUserByNameOnly(string userName)
    {
        CheckDomainNameHasValue();
        CheckDomainPasswordHasValue();
        ExceptionHelper.ThrowIfNullOrEmpty(userName);

        return FindUser(_options.Value.DomainName, userName, _options.Value.Password);
    }

    public AdUser FindUserByNameOnly(string domain, string userName)
    {
        CheckDomainPasswordHasValue();

        ExceptionHelper.ThrowIfNullOrEmpty(domain);
        ExceptionHelper.ThrowIfNullOrEmpty(userName);

        return FindUser(domain, userName, _options.Value.Password);
    }

    public AdUser FindUser(String userName, string password)
    {
        CheckDomainNameHasValue();
        ExceptionHelper.ThrowIfNullOrEmpty(userName);
        ExceptionHelper.ThrowIfNullOrEmpty(password);

        return FindUser(_options.Value.DomainName, userName, password);
    }

    public AdUser FindUser(string domain, string userName, string password)
    {
        ExceptionHelper.ThrowIfNullOrEmpty(domain);
        ExceptionHelper.ThrowIfNullOrEmpty(password);
        ExceptionHelper.ThrowIfNullOrEmpty(userName);

        try
        {
            using (var connection = _activeDirectoryManager.OpenConnection(domain, userName, password ?? _options.Value.Password))
            {

                var response = (SearchResponse)connection.SendRequest(
                    new SearchRequest(
                        ActiveDirectoryUtils.GetDomainControllerNames(domain),
                        $"SAMAccountName={userName}",
                        SearchScope.Subtree, new string[] {
                        "sAMAccountName",
                        "userPrincipalName",
                        "useraccountcontrol",
                        "displayName" ,
                        "telephoneNumber",
                        "DistinguishedName"
                        }
                    ));

                if (response.Entries.Count == 0)
                {
                    return null;
                }

                var entry = response.Entries[0];
                int.TryParse(entry.FindFirstAttributeValue("useraccountcontrol"), out int control);

                AdUser user = new AdUser()
                {
                    UserName = entry.FindFirstAttributeValue("sAMAccountName"),
                    DisplayName = entry.FindFirstAttributeValue("displayName"),
                    Email = entry.FindFirstAttributeValue("userPrincipalName"),
                    TelephoneNumber = entry.FindFirstAttributeValue("telephoneNumber"),
                    IsEnabled = (control & 2) == 0
                };

                return user;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return null;
    }

    private void CheckDomainNameHasValue() => ExceptionHelper.ThrowIfNullOrEmpty(_options.Value.DomainName);

    private void CheckDomainuserHasValue() => ExceptionHelper.ThrowIfNullOrEmpty(_options.Value.UserName);

    private void CheckDomainPasswordHasValue() => ExceptionHelper.ThrowIfNullOrEmpty(_options.Value.Password);
}
