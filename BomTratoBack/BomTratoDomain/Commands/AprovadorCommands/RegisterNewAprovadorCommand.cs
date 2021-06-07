using BomTratoDomain.Commands.Validations.Aprovador;
using System;

namespace BomTratoDomain.Commands.AprovadorCommands
{
    public class RegisterNewAprovadorCommand : AprovadorCommand
    {
        public RegisterNewAprovadorCommand(string name, string lastName, string email, DateTime birthDate)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewAprovadorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}