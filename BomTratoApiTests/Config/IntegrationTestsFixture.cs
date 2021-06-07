using BomTratoApi;
using BomTratoApiTests.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using NetDevPack.Identity.Model;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BomTratoApiTests
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    { 
        public readonly BomTratoAppFactory<TStartup> Factory;
        public HttpClient Client;
        public string UsuarioToken;
        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };
            Factory = new BomTratoAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
        }
        public async Task LoginApi()
        {
            var userData = new LoginUser
            {
                Email = "teste@teste.com",
                Password = "Teste@123"
            };
            Client = Factory.CreateClient();
            var response = await Client.PostAsJsonAsync("login", userData);
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }
        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
