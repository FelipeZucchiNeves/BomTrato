using AutoMapper;
using BomTratoApp.Models;
using BomTratoDomain.Commands.AprovadorCommands;
using BomTratoDomain.Commands.EscritorioCommands;
using BomTratoDomain.Commands.ProcessoCommands;
using BomTratoDomain.Entities;

namespace BomTratoApp.AutoMapper
{
    public class ModelToEntityMapping : Profile
    {
        public ModelToEntityMapping()
        {
            CreateMap<AprovadorViewModel, RegisterNewAprovadorCommand>()
               .ConstructUsing(c => new RegisterNewAprovadorCommand(c.Name, c.LastName, c.Email, c.BirthDate));
            CreateMap<AprovadorViewModel, UpdateAprovadorCommand>()
                .ConstructUsing(c => new UpdateAprovadorCommand(c.Id, c.Name, c.LastName, c.Email, c.BirthDate));
            CreateMap<ProcessoViewModel, RegisterNewProcessoCommand>();
            CreateMap<ProcessoViewModel, UpdateProcessoCommand>();
            CreateMap<EscritorioViewModel, RegisterNewEscritorioCommand>()
               .ConstructUsing(c => new RegisterNewEscritorioCommand(c.Street, c.Number, c.State, c.Cep, c.City, c.District));
            CreateMap<EscritorioViewModel, UpdateEscritorioCommand>()
                .ConstructUsing(c => new UpdateEscritorioCommand(c.Id, c.Street, c.Number, c.State, c.Cep, c.City, c.District));
        }
    }
}

