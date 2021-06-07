using BomTratoApi;
using BomTratoApiTests.Config;
using BomTratoApp.Models;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BomTratoApiTests
{
    [TestCaseOrderer("BomTratoApiTests.Config.PriorityOrderer", "BomTratoApiTests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class EscritorioApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;
        private const string APIPATH = "escritorio-management";
        public EscritorioApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }
        [Fact(DisplayName = "Add new Office"), TestPriority(1)]
        [Trait("Category", "Integration API - Office")]
        public async Task Office_NewOffice_ShouldReturnWithSuccess()
        {
            // Arrange
            var escritorio = new EscritorioViewModel
            {
                Street = "Rua Belo Horizonte",
                State = "SP",
                Cep  = 08574798,
                City = "Itaquaquecetuba",
                Number = "125",
                District = "Montes Claros"
            };
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync(APIPATH, escritorio);
            // Assert
            postResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Update Office"), TestPriority(2)]
        [Trait("Category", "Integration API - Office")]
        public async Task Office_UpdateOffice_ShouldReturnWithSuccess()
        {
            // Arrange
            var escritorio = new EscritorioViewModel
            {
                Street = "Rua Belo Horizonte",
                State = "SP",
                Cep = 08574798,
                City = "Itaquaquecetuba",
                Number = "125",
                District = "Montes Claros"
            };
            escritorio.Id = await GetId();
            // Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync(APIPATH, escritorio);
            // Assert
            putResponse.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "Delete Office"), TestPriority(3)]
        [Trait("Category", "Integration API - Office")]
        public async Task Office_DeleteOffice_ShouldReturnWithSuccess()
        {
            // Arrange
            Guid officeId = await GetId();
            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"{APIPATH}/{officeId}");
            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
        private async Task<Guid> GetId()
        {
            var allOffices = await _testsFixture.Client.GetAsync(APIPATH);
            var getResponse = await allOffices.Content.ReadAsStringAsync();
            var officeId = Guid.Parse(getResponse.Substring(8, 36));
            return officeId;
        }
    }
}
