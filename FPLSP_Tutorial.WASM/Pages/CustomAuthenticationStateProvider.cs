using System.Security.Claims;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace FPLSP_Tutorial.WASM.Pages;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
    private readonly ISessionStorageService _session;

    public CustomAuthenticationStateProvider(ISessionStorageService session)
    {
        _session = session;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var lstClaimVM = await _session.GetItemAsync<List<ClaimSimplifyModel>>("UserClaims");
            var lstClaim = lstClaimVM.Select(c => new Claim(c.Key, c.Value)).ToList();

            if (lstClaim == null || lstClaim.Count == 0)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(lstClaim, "CustomAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(List<Claim>? claims)
    {
        ClaimsPrincipal claimsPrincipal;

        if (claims != null)
        {
            var lstClaimVM = claims.Select(c => new ClaimSimplifyModel
            {
                Key = c.Type,
                Value = c.Value
            }).ToList();

            await _session.SetItemAsync("UserClaims", lstClaimVM);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        }
        else
        {
            await _session.RemoveItemAsync("UserClaims");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}