using BomTratoDomain.Commands.Validations.Processo;
using System;

namespace BomTratoDomain.Commands.ProcessoCommands
{
    public class RemoveProcessoCommand : ProcessoCommand
    {
        public RemoveProcessoCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveProcessoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}