using BomTratoApi;
using BomTratoApiTests.Config;
using BomTratoApp.Models;
using NetDevPack.Identity.Model;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BomTratoApiTests
{
    [TestCaseOrderer("BomTratoApiTests.Config.PriorityOrderer", "BomTratoApiTests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AprovadorApiTest
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;
        private const string APIPATH = "aprovador-management";
        public AprovadorApiTest(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }
        [Fact(DisplayName = "Add new Aprover"), TestPriority(1)]
        [Trait("Category", "Integration API - Aprover")]
        public async Task Aprover_NewAprover_ShouldReturnWithSuccess()
        {
            // Arrange
            var aprover = new AprovadorViewModel
            {
                Name = "Felipe",
                LastName = "tester",
                Email = "teste@teste.com",
                BirthDate = DateTime.Now.AddYears(-18)
            };
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(APIPATH, aprover);
            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Update Aprover"), TestPriority(4)]
        [Trait("Category", "Integration API - Aprover")]
        public async Task Aprover_UpdateAProver_ShouldReturnWithSuccess()
        {
            // Arrange
            var aprover = new AprovadorViewModel
            {
                Name = "Teste",
                LastName = "Neves",
                Email = "teste@teste.com",
                BirthDate = DateTime.Now.AddYears(-18)
            };
            aprover.Id = await GetIdAprover();
            // Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync(APIPATH, aprover);
            // Assert
            putResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Delete Aprover"), TestPriority(5)]
        [Trait("Category", "Integration API - Aprover")]
        public async Task Aprover_DeleteAProver_ShouldReturnWithSuccess()
        {
            // Arrange
            var aproverId = await GetIdAprover();
            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"{APIPATH}/{aproverId}");
            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Register with success"), TestPriority(2)]
        [Trait("Category", "Integration API - Aprover")]
        public async Task User_Register_ShouldExecuteWithSuccess()
        {
            // Arrange
            var user = new RegisterUser
            {
                Email = "teste@teste.com",
                Password = "Teste@123",
                ConfirmPassword = "Teste@123"
            };
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("register", user);
            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Login with success"), TestPriority(3)]
        [Trait("Category", "Integration API - Aprover")]
        public async Task User_Login_ShouldExecuteWithSuccess()
        {
            // Arrange
            var user = new RegisterUser
            {
                Email = "teste@teste.com",
                Password = "Teste@123",
            };
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("login", user);
            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
        private async Task<Guid> GetIdAprover()
        {
            var allAprovers = await _testsFixture.Client.GetAsync(APIPATH);
            var getResponse = await allAprovers.Content.ReadAsStringAsync();
            return Guid.Parse(getResponse.Substring(8, 36));
        }
    }
}
