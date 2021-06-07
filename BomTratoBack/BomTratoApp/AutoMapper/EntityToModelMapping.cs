using AutoMapper;
using BomTratoApp.Models;
using BomTratoDomain.Entities;

namespace BomTratoApp.AutoMapper
{
    public class EntityToModelMapping : Profile
    {
        public EntityToModelMapping()
        {
            CreateMap<Aprovador, AprovadorViewModel>();
            CreateMap<Processo, ProcessoViewModel>();
            CreateMap<Escritorio, EscritorioViewModel>();
        }
    }
}
