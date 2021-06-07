using BomTratoDomain.Commands.EscritorioCommands;
using BomTratoDomain.Commands.Validations.Escritorio;
using BomTratoDomain.Entities;
using BomTratoDomain.Interfaces;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BomTratoDomainTests
{
    public class EscritorioCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly Escritorio _escritorio;
        private readonly EscritorioCommandHandler _escritorioHandler;
        public EscritorioCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _escritorio = new Escritorio();
            _escritorioHandler = _mocker.CreateInstance<EscritorioCommandHandler>();
        }
        [Fact(DisplayName = "Aprovador registered with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Register_NewAprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var escritorioCommand = new RegisterNewEscritorioCommand("Rua abc", "82A", "BA",08578989,"Itaqua","monte morto");
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _escritorioHandler.Handle(escritorioCommand, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.Add(It.IsAny<Escritorio>()), Times.Once);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador updated with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Update_Aprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var escritorioCommand = new RegisterNewEscritorioCommand("Rua abc", "82A", "BA", 08578989, "Itaqua", "monte morto");
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.GetById(escritorioCommand.Id)).Returns(Task.FromResult(_escritorio));
            var escritorio = new UpdateEscritorioCommand(_escritorio.Id, "xxxx", "103", "SP",08578987,"itaqua","monte morto");
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _escritorioHandler.Handle(escritorio, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.Update(It.IsAny<Escritorio>()), Times.Once);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador removed with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Remove_Aprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var escritorioCommand = new RegisterNewEscritorioCommand("Rua abc", "82A", "BA", 08578989, "Itaqua", "monte morto");
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.GetById(escritorioCommand.Id)).Returns(Task.FromResult(_escritorio));
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.GetById(_escritorio.Id)).Returns(Task.FromResult(_escritorio));
            var aprovador = new RemoveEscritorioCommand(_escritorio.Id);
            // Act
            var result = await _escritorioHandler.Handle(aprovador, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.Remove(It.IsAny<Escritorio>()), Times.Once);
            _mocker.GetMock<IEscritorioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador invalid register command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void RegisterNewAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new RegisterNewEscritorioCommand(string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StreetErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StreetLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StateErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StateLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.NumberErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.NumberLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.CityErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.CityLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.DistrictErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.DistrictLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
        [Fact(DisplayName = "Aprovador invalid update command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void UpdateAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new UpdateEscritorioCommand(Guid.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(EscritorioValidation<EscritorioCommand>.IdEscritorioErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StreetErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StreetLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StateErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.StateLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.NumberErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.NumberLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.CityErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.CityLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.DistrictErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(EscritorioValidation<EscritorioCommand>.DistrictLengthErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
        [Fact(DisplayName = "Aprovador invalid remove command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void RemoveAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new RemoveEscritorioCommand(Guid.Empty);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(EscritorioValidation<EscritorioCommand>.IdEscritorioErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
