using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;


public class PersistingRemoteAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IAccessTokenProvider _tokenProvider;

    public PersistingRemoteAuthenticationStateProvider(IAccessTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenResult = await _tokenProvider.RequestAccessToken();
        if (tokenResult.TryGetToken(out var token))
        {
            var identity = new ClaimsIdentity("Bearer");
            identity.AddClaim(new Claim(ClaimTypes.Name, "TestUser"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Dispecink")); // Testovací role

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}
