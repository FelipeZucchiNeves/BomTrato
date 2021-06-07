using AutoMapper;
using BomTratoApp.Models;
using BomTratoApp.Services.Interfaces;
using BomTratoData.Repository.EventSourcing;
using BomTratoDomain.Commands.ProcessoCommands;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IMapper _mapper;
        private readonly IProcessoRepository _ProcessoRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;
        public ProcessoService(
            IMapper mapper,
            IProcessoRepository ProcessoRepository,
            IEventStoreRepository eventStoreRepository,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _ProcessoRepository = ProcessoRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<ProcessoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProcessoViewModel>>(await _ProcessoRepository.GetAll());
        }
        public async Task<ProcessoViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProcessoViewModel>(await _ProcessoRepository.GetById(id));
        }
        public async Task<ValidationResult> Register(ProcessoViewModel ProcessoViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewProcessoCommand>(ProcessoViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveProcessoCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }
        public async Task<ValidationResult> Update(ProcessoViewModel ProcessoViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProcessoCommand>(ProcessoViewModel);
            return await _mediator.SendCommand(updateCommand);
        }
    }
}
