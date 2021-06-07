using BomTratoDomain.Commands.Validations.Escritorio;
using FluentValidation.Results;
using System;

namespace BomTratoDomain.Commands.EscritorioCommands
{
    public class RemoveEscritorioCommand : EscritorioCommand
    {
        public RemoveEscritorioCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveEscritorioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
