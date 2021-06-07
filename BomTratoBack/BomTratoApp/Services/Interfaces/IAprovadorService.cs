using BomTratoApp.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoApp.Services.Interfaces
{
    public interface IAprovadorService : IDisposable
    {
        Task<IEnumerable<AprovadorViewModel>> GetAll();
        Task<AprovadorViewModel> GetById(Guid id);
        Task<AprovadorViewModel> GetByEmail(string email);
        Task<ValidationResult> Remove(Guid id);
        Task<ValidationResult> Register(AprovadorViewModel aprovadorViewModel);
        Task<ValidationResult> Update(AprovadorViewModel aprovadorViewModel);
    }
}
