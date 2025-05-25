using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using SAVIAQUA.Core.App;

namespace SAVIAQUA.API.Filters;

public class AuthFilter : IAuthorizationFilter
{
    private readonly Session _session;
    
    public AuthFilter(
        Session session
    )
    {
        _session = session;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = await GetTokenInformation(context.HttpContext);
            
        var userId = GetClaim(token, "UId");

        _session.CodigoUsuario = Int16.Parse(userId);
    }
        
    private async Task<IEnumerable<Claim>> GetTokenInformation(HttpContext httpContext)
    {
        var jwt = await httpContext.GetTokenAsync("access_token");
        
        if (string.IsNullOrWhiteSpace(jwt))
        {
            throw new UnauthorizedAccessException("JWT not valid");
        }
        
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return token.Claims;
    }
        
    private string GetClaim(IEnumerable<Claim> token, string claimName)
    {
        var claim = token
            .Where(c => c.Type == claimName)
            .Select(c => c.Value).SingleOrDefault();

        return claim ?? string.Empty;
    }
}