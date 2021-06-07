using AutoMapper;
using BomTratoDomain.Entities;
using BomTratoDomain.Events.ProcessoEvents;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BomTratoDomain.Commands.ProcessoCommands
{
    public class ProcessoCommandHandler : CommandHandler, 
        IRequestHandler<RegisterNewProcessoCommand, ValidationResult>,
        IRequestHandler<UpdateProcessoCommand, ValidationResult>,
        IRequestHandler<RemoveProcessoCommand, ValidationResult>
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IAprovadorRepository _aprovadorRepository;
        private readonly IEscritorioRepository _escritorioRepository;
        private readonly IMapper _mapper;
        public ProcessoCommandHandler(IProcessoRepository processoRepository,
                                      IAprovadorRepository aprovadorRepository,
                                      IEscritorioRepository escritorioRepository,
                                      IMapper mapper)
        {
            _processoRepository = processoRepository;
            _aprovadorRepository = aprovadorRepository;
            _escritorioRepository = escritorioRepository;
            _mapper = mapper;
        }
        public async Task<ValidationResult> Handle(UpdateProcessoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var processo = new Processo(request.Id, request.ProcessNumber, request.Value, request.AprovadorId, request.EscritorioId, request.Aproved, request.Status, request.ComplainerName);
            var aprovador = _aprovadorRepository.GetById(processo.AprovadorId);
            var escritorio = _escritorioRepository.GetById(processo.EscritorioId);
            if (aprovador is null || escritorio is null)
            {
                AddError("Erro ao atualizar o registro verifique os dados inseridos");
                return ValidationResult;
            }
            var existingProcesso = await _processoRepository.GetById(processo.Id);

            if(existingProcesso != null && existingProcesso.Id != processo.Id)
            {
                if (!existingProcesso.Equals(processo))
                {
                    AddError("O processo á está atualizado");
                    return ValidationResult;
                }
            }
            processo.AddDomainEvent(new ProcessoUpdatedEvent(processo.Id,processo.ProcessNumber, processo.Value, processo.Escritorio,
                                                             processo.Aprovador, processo.AprovadorId, processo.EscritorioId, 
                                                             processo.Aproved, processo.Status, processo.ComplainerName));
            _processoRepository.Update(processo);
            return await Commit(_processoRepository.UnitOfWork);
        }
        public async  Task<ValidationResult> Handle(RegisterNewProcessoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var processo = new Processo(Guid.NewGuid(), request.ProcessNumber, request.Value, request.AprovadorId, 
                                        request.EscritorioId, request.Aproved, request.Status, request.ComplainerName);
            var aprovador = await _aprovadorRepository.GetById(processo.AprovadorId);
            var escritorio = await _escritorioRepository.GetById(processo.EscritorioId);
            if (aprovador is null || escritorio is null)
            {
                AddError("Erro ao atualizar o registro verifique os dados inseridos");
                return ValidationResult;
            }
            if (await _processoRepository.GetByProcessNumber(processo.ProcessNumber) != null)
            {
                AddError("Processo já foi cadastrado");
                return ValidationResult;
            }
            processo.AddDomainEvent(new ProcessoRegisteredEvent(Guid.NewGuid(), processo.ProcessNumber, processo.Value, processo.AprovadorId, 
                                                                processo.EscritorioId, processo.Aproved, processo.Status, processo.ComplainerName));
            _processoRepository.Add(processo);
            return await Commit(_processoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoveProcessoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;
            var processo = await _processoRepository.GetById(request.Id);
            if(processo is null)
            {
                AddError("O Processo não existe.");
                return ValidationResult;
            }
            processo.AddDomainEvent(new ProcessoRemovedEvent(request.Id));
            _processoRepository.Remove(processo);
            return await Commit(_processoRepository.UnitOfWork);
        }
    }
}
