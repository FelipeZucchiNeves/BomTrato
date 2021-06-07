using BomTratoApp.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services.Interfaces
{
    public interface IEscritorioService
    {
        Task<IEnumerable<EscritorioViewModel>> GetAll();
        Task<EscritorioViewModel> GetById(Guid id);
        Task<ValidationResult> Remove(Guid id);
        Task<ValidationResult> Register(EscritorioViewModel EscritorioViewModel);
        Task<ValidationResult> Update(EscritorioViewModel EscritorioViewModel);
    }
}
