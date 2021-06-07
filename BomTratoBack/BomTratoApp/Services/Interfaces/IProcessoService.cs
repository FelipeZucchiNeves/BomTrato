using BomTratoApp.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services.Interfaces
{
    public interface IProcessoService : IDisposable
    {
        Task<IEnumerable<ProcessoViewModel>> GetAll();
        Task<ProcessoViewModel> GetById(Guid id);
        Task<ValidationResult> Remove(Guid id);
        Task<ValidationResult> Register(ProcessoViewModel ProcessoViewModel);
        Task<ValidationResult> Update(ProcessoViewModel ProcessoViewModel);
    }
}
