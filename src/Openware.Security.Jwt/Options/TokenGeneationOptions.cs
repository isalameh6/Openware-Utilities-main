using Microsoft.IdentityModel.Tokens;

namespace Openware.Security.Jwt;
public class TokenGeneationOptions
{
    public TokenValidationParameters TokenValidationParameters { get; set; }
    public TimeSpan? ExpiresAfter { get; set; }
}
