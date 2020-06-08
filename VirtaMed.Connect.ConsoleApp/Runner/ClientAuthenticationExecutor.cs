using IdentityModel.Client;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VirtaMed.Connect.ConsoleApp.Runner
{
    class ClientAuthenticationExecutor : IExecutor
    {
        public async Task Execute()
        {
            var tokenClient = new HttpClient();

            var disco = await tokenClient.GetDiscoveryDocumentAsync("http://localhost:6000/");
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await tokenClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Connect.Console",
                ClientSecret = "secret"
            });

            if (response.IsError) throw new Exception(response.Error);

            var apiClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };
            apiClient.SetBearerToken(response.AccessToken);

            var apiResult =  await apiClient.GetStringAsync("identitytest/checkAuthentication");
        }
    }
}
