using IdentityModel.Client;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VirtaMed.Connect.ConsoleApp.Runner
{
    class PasswordAuthenticationExecutor : IExecutor
    {
        public async Task Execute()
        {
            var tokenClient = new HttpClient();

            var disco = await tokenClient.GetDiscoveryDocumentAsync("http://localhost:6000/");
            if (disco.IsError) throw new Exception(disco.Error);

            var tokenResponse = await tokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                UserName = "alice",
                Password = "alice",
                Address = disco.TokenEndpoint,
                ClientId = "Connect.Console.Password",
                ClientSecret = "secret",
                Scope = "openid profile Connect.API email"
            });

            if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

            var userInfoResponse = await tokenClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            });

            var apiClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
                //BaseAddress = new Uri("http://localhost:53407/api/")
            };
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var apiResult =  await apiClient.GetStringAsync("identitytest/checkAuthentication");
        }
    }
}
