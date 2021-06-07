using AutoMapper;
using BomTratoApp.Models;
using BomTratoApp.Services.Interfaces;
using BomTratoData.Repository.EventSourcing;
using BomTratoDomain.Commands.AprovadorCommands;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services
{
    public class AprovadorService : IAprovadorService
    {
        private readonly IMapper _mapper;
        private readonly IAprovadorRepository _aprovadorRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public AprovadorService(
            IMapper mapper,
            IAprovadorRepository aprovadorRepository,
            IEventStoreRepository eventStoreRepository,
            IMediatorHandler mediator)
        {
            _mapper = mapper;
            _aprovadorRepository = aprovadorRepository;
            _eventStoreRepository = eventStoreRepository;
            _mediator = mediator;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<AprovadorViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<AprovadorViewModel>>(await _aprovadorRepository.GetAll());
        }
        public async Task<AprovadorViewModel> GetById(Guid id)
        {
            return _mapper.Map<AprovadorViewModel>(await _aprovadorRepository.GetById(id));
        }
        public async Task<AprovadorViewModel> GetByEmail(string email)
        {
            return _mapper.Map<AprovadorViewModel>(await _aprovadorRepository.GetByEmail(email));
        }
        public async Task<ValidationResult> Register(AprovadorViewModel aprovadorViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewAprovadorCommand>(aprovadorViewModel);
            return await _mediator.SendCommand(registerCommand);
        }
        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveAprovadorCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }
        public async Task<ValidationResult> Update(AprovadorViewModel aprovadorViewModel)
        {
            var updateCommand = _mapper.Map<UpdateAprovadorCommand>(aprovadorViewModel);
            return await _mediator.SendCommand(updateCommand);
        }
    }
}
