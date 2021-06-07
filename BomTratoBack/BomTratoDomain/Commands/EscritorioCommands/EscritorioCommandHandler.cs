using AutoMapper;
using BomTratoDomain.Entities;
using BomTratoDomain.Events.EscritorioEvents;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Commands.EscritorioCommands
{
    public class EscritorioCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewEscritorioCommand, ValidationResult>,
        IRequestHandler<UpdateEscritorioCommand, ValidationResult>,
        IRequestHandler<RemoveEscritorioCommand, ValidationResult>
    {
        private readonly IEscritorioRepository _EscritorioRepository;
        private readonly IMapper _mapper;
        public EscritorioCommandHandler(IEscritorioRepository EscritorioRepository, IMapper mapper)
        {
            _EscritorioRepository = EscritorioRepository;
            _mapper = mapper;
        }
        public async Task<ValidationResult> Handle(UpdateEscritorioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var escritorio = new Escritorio(request.Id, request.Street, request.Number, request.State, request.Cep, request.City, request.District) ;
            var existingEscritorio = await _EscritorioRepository.GetById(escritorio.Id);
            if (existingEscritorio != null && existingEscritorio.Id != escritorio.Id)
            {
                if (!existingEscritorio.Equals(escritorio))
                {
                    AddError("Escritório já cadastrado");
                    return ValidationResult;
                }
            }
            escritorio.AddDomainEvent(new EscritorioUpdatedEvent(escritorio.Id, escritorio.Street, escritorio.State, escritorio.Number));
            _EscritorioRepository.Update(escritorio);
            return await Commit(_EscritorioRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(RegisterNewEscritorioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var escritorio = new Escritorio(Guid.NewGuid(), request.Street, request.Number, request.State, request.Cep, request.City, request.District);
            if (await _EscritorioRepository.GetById(escritorio.Id) != null)
            {
                AddError("Escritório já registrado");
                return ValidationResult;
            }
            escritorio.AddDomainEvent(new EscritorioRegisteredEvent(escritorio.Id, escritorio.Street, escritorio.State, escritorio.Number));
            _EscritorioRepository.Add(escritorio);
            return await Commit(_EscritorioRepository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(RemoveEscritorioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var Escritorio = await _EscritorioRepository.GetById(request.Id);
            if (Escritorio is null)
            {
                AddError("Escritório não cadastrado");
                return ValidationResult;
            }
            Escritorio.AddDomainEvent(new EscritorioRemovedEvent(request.Id));
            _EscritorioRepository.Remove(Escritorio);
            return await Commit(_EscritorioRepository.UnitOfWork);
        }
    }
}
