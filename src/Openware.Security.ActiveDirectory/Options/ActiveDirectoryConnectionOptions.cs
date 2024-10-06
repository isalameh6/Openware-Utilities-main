using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Openware.Security.ActiveDirectory;

/// <summary>
/// A type to configure default connection details for Active Directory
/// </summary>
public class ActiveDirectoryConnectionOptions
{
    /// <summary>
    /// The domain name
    /// </summary>
    public string DomainName { get; set; }
    /// <summary>
    /// The user name
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// The password
    /// </summary>
    public string Password { get; set; }
}
