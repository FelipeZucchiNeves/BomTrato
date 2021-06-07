using BomTratoDomain.Commands.AprovadorCommands;
using BomTratoDomain.Commands.Validations.Aprovador;
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
    public class AprovadorCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly Aprovador _aprovador;
        private readonly AprovadorCommandHandler _aprovadorHandler;
        public AprovadorCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _aprovador = new Aprovador();
            _aprovadorHandler = _mocker.CreateInstance<AprovadorCommandHandler>();
        }
        [Fact(DisplayName = "Aprovador registered with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Register_NewAprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var aprovadorCommand = new RegisterNewAprovadorCommand("Felipe", "Neves", "felipe@blabla.com",DateTime.Now.AddYears(-18));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _aprovadorHandler.Handle(aprovadorCommand, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.Add(It.IsAny<Aprovador>()), Times.Once);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador updated with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Update_Aprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var aprovadorCommand = new RegisterNewAprovadorCommand("Felipe", "Neves", "felipe@blabla.com", DateTime.Now.AddYears(-18));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.GetByEmail(aprovadorCommand.Email)).Returns(Task.FromResult(_aprovador));
            var aprovador = new UpdateAprovadorCommand(_aprovador.Id, "xxxx", "xxxx", "felipe@blabla.com", DateTime.Now.AddYears(-18));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _aprovadorHandler.Handle(aprovador, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.Update(It.IsAny<Aprovador>()), Times.Once);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador removed with Success")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public async Task Remove_Aprovador_ShouldExecuteWithSuccess()
        {
            // Arrange
            var aprovadorCommand = new RegisterNewAprovadorCommand("Felipe", "Neves", "felipe@blabla.com", DateTime.Now.AddYears(-18));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.GetByEmail(aprovadorCommand.Email)).Returns(Task.FromResult(_aprovador));
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.GetById(_aprovador.Id)).Returns(Task.FromResult(_aprovador));
            var aprovador = new RemoveAprovadorCommand(_aprovador.Id);
            // Act
            var result = await _aprovadorHandler.Handle(aprovador, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.Remove(It.IsAny<Aprovador>()), Times.Once);
            _mocker.GetMock<IAprovadorRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Aprovador invalid register command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void RegisterNewAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new RegisterNewAprovadorCommand("", "", "", DateTime.Now);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(AprovadorValidation<AprovadorCommand>.NameLasNameErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.NameLastNameLenghtErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.EmailErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.BirthDateErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
        [Fact(DisplayName = "Aprovador invalid update command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void UpdateAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new UpdateAprovadorCommand(Guid.Empty, "", "", "", DateTime.Now);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(AprovadorValidation<AprovadorCommand>.IdAprovadorErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.NameLasNameErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.NameLastNameLenghtErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.EmailErrorMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AprovadorValidation<AprovadorCommand>.BirthDateErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
        [Fact(DisplayName = "Aprovador invalid remove command")]
        [Trait("Category", "Aprovador - Aprovador Command Handler")]
        public void RemoveAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var aprovadorCommand = new RemoveAprovadorCommand(Guid.Empty);
            // Act
            var result = aprovadorCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(AprovadorValidation<AprovadorCommand>.IdAprovadorErroMsg, aprovadorCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
