using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace BomTratoApi.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> {
                {"Message", _errors.ToArray() }
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary model)
        {
            var errors = model.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse();
        }
        protected ActionResult CustomResponse (ValidationResult validation)
        {
            foreach (var error in validation.Errors)
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse();
        }
        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }
        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }
        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
