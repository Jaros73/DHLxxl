using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;

public class FakeAccessTokenProvider : IAccessTokenProvider
{
    public ValueTask<AccessTokenResult> RequestAccessToken()
    {
        var fakeToken = new AccessToken
        {
            Value = "fake-token",
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        };

        var interactiveOptions = new InteractiveRequestOptions
        {
            Interaction = InteractionType.GetToken,  // Povinný parametr
            ReturnUrl = "/"  // Nastaveno na domovskou stránku
        };

        return ValueTask.FromResult(new AccessTokenResult(
            AccessTokenResultStatus.Success,
            fakeToken,
            string.Empty, // Místo `null`
            interactiveOptions
        ));
    }

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
    {
        return RequestAccessToken();
    }
}
