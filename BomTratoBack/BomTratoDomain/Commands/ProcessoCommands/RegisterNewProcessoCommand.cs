using BomTratoDomain.Commands.Validations.Processo;
using System;

namespace BomTratoDomain.Commands.ProcessoCommands
{
    public class RegisterNewProcessoCommand : ProcessoCommand
    {
        public RegisterNewProcessoCommand(string processNumber, decimal value,Guid aprovadorId, Guid escritorioId, bool aproved, bool status, string complainerName)
        {
            ProcessNumber = processNumber;
            Value = value;
            AprovadorId = aprovadorId;
            EscritorioId = escritorioId;
            Aproved = aproved;
            Status = status;
            ComplainerName = complainerName;
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewProcessoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}