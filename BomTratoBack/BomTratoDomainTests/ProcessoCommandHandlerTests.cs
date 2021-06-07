using BomTratoDomain.Commands.ProcessoCommands;
using BomTratoDomain.Commands.Validations.Processo;
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
    public class ProcessoCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly Processo _processo;
        private readonly Aprovador _aprovador;
        private readonly Escritorio _escritorio;
        private readonly ProcessoCommandHandler _processoHandler;
        public ProcessoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _aprovador = new Aprovador(Guid.NewGuid(), "Felipe", "Neves", "felipe@blabla.com", DateTime.Now.AddYears(-18));
            _escritorio = new Escritorio(Guid.NewGuid(), "rua x", "82", "SP", 08579855,"sanduba","queijo");
            _processoHandler = _mocker.CreateInstance<ProcessoCommandHandler>();
            _processo = new Processo();
        }
        [Fact(DisplayName = "Processo registered with Success")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public async Task Register_NewProcesso_ShouldExecuteWithSuccess()
        {
            // Arrange
            var processoCommand = new RegisterNewProcessoCommand("111111111111", 30000, _aprovador.Id, _escritorio.Id, false, false, "Felipe");
            _mocker.GetMock<IAprovadorRepository>().Setup(r => r.GetById(_aprovador.Id)).Returns(Task.FromResult(_aprovador));
            _mocker.GetMock<IEscritorioRepository>().Setup(r => r.GetById(_escritorio.Id)).Returns(Task.FromResult(_escritorio));
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _processoHandler.Handle(processoCommand, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.Add(It.IsAny<Processo>()), Times.Once);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Processo updated with Success")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public async Task Update_Processo_ShouldExecuteWithSuccess()
        {
            // Arrange
            var processoCommand = new RegisterNewProcessoCommand("111111111111", 30000, _aprovador.Id, _escritorio.Id, false, false, "Felipe");
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.GetByProcessNumber(processoCommand.ProcessNumber)).Returns(Task.FromResult(_processo));
            var processo = new UpdateProcessoCommand(_processo.Id,"222222222222", 35000,  _aprovador.Id, _escritorio.Id, false, false, "Felipe Neves");
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            // Act
            var result = await _processoHandler.Handle(processo, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.Update(It.IsAny<Processo>()), Times.Once);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Processo removed with Success")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public async Task Remove_Processo_ShouldExecuteWithSuccess()
        {
            // Arrange
            var processoCommand = new RegisterNewProcessoCommand("111111111111", 30000, _aprovador.Id, _escritorio.Id, false, false, "Felipe");
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.GetByProcessNumber(processoCommand.ProcessNumber)).Returns(Task.FromResult(_processo));
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.GetById(_processo.Id)).Returns(Task.FromResult(_processo));
            _mocker.GetMock<IProcessoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var processo = new RemoveProcessoCommand(_processo.Id);
            // Act
            var result = await _processoHandler.Handle(processo, CancellationToken.None);
            // Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.Remove(It.IsAny<Processo>()), Times.Once);
            _mocker.GetMock<IProcessoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
        [Fact(DisplayName = "Processo invalid register command")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public void RegisterNewProcessoCommand_Processo_ShouldNotPassInNValidations()
        {
            // Arrange
            var processoCommand = new RegisterNewProcessoCommand("", 0, Guid.Empty, Guid.Empty, false, false, "");
            // Act
            var result = processoCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ProcessNumberErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ComplainerNameErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.EscritorioErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ValueErroMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ComplainerNameLenghtErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.AprovadorErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
        [Fact(DisplayName = "Processo invalid update command")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public void UpdateAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var processoCommand = new UpdateProcessoCommand(Guid.Empty, "", 0, Guid.Empty, Guid.Empty, false, false, "");
            // Act
            var result = processoCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(ProcessoValidation<ProcessoCommand>.IdErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ProcessNumberErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ComplainerNameErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.EscritorioErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ValueErroMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.AprovadorErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(ProcessoValidation<ProcessoCommand>.ComplainerNameLenghtErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Processo invalid remove command")]
        [Trait("Category", "Processo - Processo Command Handler")]
        public void RemoveAprovadorCommand_Aprovador_ShouldNotPassInNValidations()
        {
            // Arrange
            var processoCommand = new RemoveProcessoCommand(Guid.Empty);
            // Act
            var result = processoCommand.IsValid();
            // Assert
            Assert.False(result);
            Assert.Contains(ProcessoValidation<ProcessoCommand>.IdErrorMsg, processoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
