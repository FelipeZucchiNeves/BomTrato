using BomTratoDomain.Commands.Validations.Aprovador;
using System;

namespace BomTratoDomain.Commands.AprovadorCommands
{
    public class RemoveAprovadorCommand : AprovadorCommand
    {
        public RemoveAprovadorCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
        public override bool IsValid()
        {
            ValidationResult = new RemoveAprovadorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}