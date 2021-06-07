using BomTratoDomain.Commands.Validations.Processo;
using System;

namespace BomTratoDomain.Commands.ProcessoCommands
{
    public class UpdateProcessoCommand : ProcessoCommand
    {
        public UpdateProcessoCommand(Guid id, string processNumber, decimal value, Guid aprovadorId, Guid escritorioId, bool aproved, bool status, string complainerName)
        {
            Id = id;
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
            ValidationResult = new UpdateProcessoCommandValidation().Validate(this);
            return base.IsValid();
        }
    }
}