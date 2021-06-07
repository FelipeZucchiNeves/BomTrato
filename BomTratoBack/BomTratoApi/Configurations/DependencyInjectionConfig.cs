using BomTratoApp.Services;
using BomTratoApp.Services.Interfaces;
using BomTratoData.Context;
using BomTratoData.CrossCutting;
using BomTratoData.EventSource;
using BomTratoData.Repository;
using BomTratoData.Repository.EventSourcing;
using BomTratoDomain.Commands.AprovadorCommands;
using BomTratoDomain.Commands.EscritorioCommands;
using BomTratoDomain.Commands.ProcessoCommands;
using BomTratoDomain.Events.AprovadorEvents;
using BomTratoDomain.Events.EscritorioEvents;
using BomTratoDomain.Events.Interfaces;
using BomTratoDomain.Events.ProcessoEvents;
using BomTratoDomain.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Mediator;
using System;

namespace BomTratoApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            // Application
            services.AddScoped<IAprovadorService, AprovadorService>();
            services.AddScoped<IProcessoService, ProcessoService>();
            services.AddScoped<IEscritorioService, EscritorioService>();
            // Domain - Events
            services.AddScoped<INotificationHandler<ProcessoRegisteredEvent>, ProcessoEventHandler>();
            services.AddScoped<INotificationHandler<ProcessoUpdatedEvent>, ProcessoEventHandler>();
            services.AddScoped<INotificationHandler<ProcessoRemovedEvent>, ProcessoEventHandler>();
            services.AddScoped<INotificationHandler<AprovadorRegisteredEvent>, AprovadorEventHandler>();
            services.AddScoped<INotificationHandler<AprovadorUpdatedEvent>, AprovadorEventHandler>();
            services.AddScoped<INotificationHandler<AprovadorRemovedEvent>, AprovadorEventHandler>();
            services.AddScoped<INotificationHandler<EscritorioRegisteredEvent>, EscritorioEventHandler>();
            services.AddScoped<INotificationHandler<EscritorioUpdatedEvent>, EscritorioEventHandler>();
            services.AddScoped<INotificationHandler<EscritorioRemovedEvent>, EscritorioEventHandler>();
            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewProcessoCommand, ValidationResult>, ProcessoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProcessoCommand, ValidationResult>, ProcessoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProcessoCommand, ValidationResult>, ProcessoCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewAprovadorCommand, ValidationResult>, AprovadorCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAprovadorCommand, ValidationResult>, AprovadorCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAprovadorCommand, ValidationResult>, AprovadorCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterNewEscritorioCommand, ValidationResult>, EscritorioCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEscritorioCommand, ValidationResult>, EscritorioCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveEscritorioCommand, ValidationResult>, EscritorioCommandHandler>();
            // Infra - Data
            services.AddScoped<IAprovadorRepository, AprovadorRepository>();
            services.AddScoped<IProcessoRepository, ProcessoRepository>();
            services.AddScoped<IEscritorioRepository, EscritorioRepository>();
            services.AddScoped<BomtratoContext>();
            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<EventStoreContext>();
        }
    }
}
