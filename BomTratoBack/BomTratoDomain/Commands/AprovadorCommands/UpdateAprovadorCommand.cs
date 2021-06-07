using BomTratoDomain.Commands.Validations.Aprovador;
using System;

namespace BomTratoDomain.Commands.AprovadorCommands
{
    public class UpdateAprovadorCommand : AprovadorCommand
    {
        public UpdateAprovadorCommand(Guid id, string name, string lastName, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateAprovadorCommandValidation().Validate(this);
            return base.IsValid();
        }
    }
}