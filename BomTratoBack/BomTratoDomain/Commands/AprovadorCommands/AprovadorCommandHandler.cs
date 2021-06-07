using AutoMapper;
using BomTratoDomain.Entities;
using BomTratoDomain.Events.AprovadorEvents;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Commands.AprovadorCommands
{
    public class AprovadorCommandHandler : CommandHandler, 
        IRequestHandler<RegisterNewAprovadorCommand, ValidationResult>,
        IRequestHandler<UpdateAprovadorCommand, ValidationResult>,
        IRequestHandler<RemoveAprovadorCommand, ValidationResult>
    {
        private readonly IAprovadorRepository _aprovadorRepository;
        private readonly IMapper _mapper;
        public AprovadorCommandHandler(IAprovadorRepository aprovadorRepository, IMapper mapper)
        {
            _aprovadorRepository = aprovadorRepository;
            _mapper = mapper;
        }
        public async Task<ValidationResult> Handle(UpdateAprovadorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var aprovador = new Aprovador(request.Id, request.Name, request.LastName, request.Email, request.BirthDate);
            var existingAprovador = await _aprovadorRepository.GetByEmail(aprovador.Email);
            if(existingAprovador != null && existingAprovador.Id != aprovador.Id)
            {
                if (!existingAprovador.Equals(aprovador))
                {
                    AddError("Aprovador já esta registrado");
                    return ValidationResult;
                }
            }
            aprovador.AddDomainEvent(new AprovadorUpdatedEvent(aprovador.Id, aprovador.Name, aprovador.LastName, aprovador.Email, aprovador.BirthDate));
            _aprovadorRepository.Update(aprovador);
            return await Commit(_aprovadorRepository.UnitOfWork);
        }
        public async  Task<ValidationResult> Handle(RegisterNewAprovadorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var aprovador = new Aprovador(Guid.NewGuid(), request.Name, request.LastName, request.Email, request.BirthDate);
            if(await _aprovadorRepository.GetByEmail(aprovador.Email) != null)
            {
                AddError("Aprovador já esta registrado");
                return ValidationResult;
            }
            aprovador.AddDomainEvent(new AprovadorRegisteredEvent(aprovador.Id, aprovador.Name, aprovador.LastName, aprovador.Email, aprovador.BirthDate));
            _aprovadorRepository.Add(aprovador);
            return await Commit(_aprovadorRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(RemoveAprovadorCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var aprovador = await _aprovadorRepository.GetById(request.Id);
            if(aprovador is null)
            {
                AddError("Esse Aprovador não está cadastrado");
                return ValidationResult;
            }
            aprovador.AddDomainEvent(new AprovadorRemovedEvent(request.Id));
            _aprovadorRepository.Remove(aprovador);
            return await Commit(_aprovadorRepository.UnitOfWork);
        }
    }
}
