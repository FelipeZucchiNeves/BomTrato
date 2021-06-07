using AutoMapper;
using BomTratoApp.Models;
using BomTratoApp.Services.Interfaces;
using BomTratoData.Repository.EventSourcing;
using BomTratoDomain.Commands.EscritorioCommands;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services
{
    public class EscritorioService : IEscritorioService
    {
        private readonly IMapper _mapper;
        private readonly IEscritorioRepository _escritorioRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;
        public EscritorioService(
            IMapper mapper,
            IEscritorioRepository escritorioRepository,
            IEventStoreRepository eventStoreRepository,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _escritorioRepository = escritorioRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<EscritorioViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<EscritorioViewModel>>(await _escritorioRepository.GetAll());
        }        public async Task<EscritorioViewModel> GetById(Guid id)
        {
            return _mapper.Map<EscritorioViewModel>(await _escritorioRepository.GetById(id));
        }
        public async Task<ValidationResult> Register(EscritorioViewModel escritorioViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewEscritorioCommand>(escritorioViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveEscritorioCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }
        public async Task<ValidationResult> Update(EscritorioViewModel EscritorioViewModel)
        {
            var updateCommand = _mapper.Map<UpdateEscritorioCommand>(EscritorioViewModel);
            return await _mediator.SendCommand(updateCommand);
        }
    }
}
