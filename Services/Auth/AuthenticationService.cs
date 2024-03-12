﻿using Microsoft.Identity.Client;
using RepRepair.Extensions;
using RepRepair.Pages;

namespace RepRepair.Services.Auth
{
    public class AuthenticationService
    {
        private IPublicClientApplication _publicClientApplication;
        private string[] _scopes = { "api://dff3c905-f0d7-4071-99cf-9cb059eb6fcd/User.Read" };
        public AuthenticationService()
        {

            if (this._publicClientApplication == null)
            {
                if (_publicClientApplication == null)
                {
#if ANDROID
                    _publicClientApplication = PublicClientApplicationBuilder
                        .Create(Constants.ApplicationId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                        .WithRedirectUri($"msal{Constants.ApplicationId}://auth")
                        .WithParentActivityOrWindow(() => Platform.CurrentActivity)
                        .Build();
#elif IOS
                    _publicClientApplication = PublicClientApplicationBuilder
                        .Create(Constants.ApplicationId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                        .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                        .WithRedirectUri($"msal{Constants.ApplicationId}://auth")
                        .Build();
#else
                    _publicClientApplication = PublicClientApplicationBuilder
                        .Create(Constants.ApplicationId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                        .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                        .Build();
#endif
                }

            }
        }
        
        public async Task<AuthenticationResult> AcquireTokenSilentAsync()
        {
            var accounts = await _publicClientApplication.GetAccountsAsync();
            return await _publicClientApplication.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                .ExecuteAsync();
        }

        public async Task<AuthenticationResult> SignInAsync()
        {
            return await _publicClientApplication.AcquireTokenInteractive(_scopes).ExecuteAsync();
        }

        public async Task SignOutAsync()
        {
            var accounts = await _publicClientApplication.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await _publicClientApplication.RemoveAsync(account);
            }
            await Shell.Current.GoToAsync(nameof(SignInPage));
        }
    }
}