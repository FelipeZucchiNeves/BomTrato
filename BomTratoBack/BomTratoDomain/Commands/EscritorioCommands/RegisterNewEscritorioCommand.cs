using BomTratoDomain.Commands.Validations.Escritorio;
using FluentValidation.Results;

namespace BomTratoDomain.Commands.EscritorioCommands
{
    public class RegisterNewEscritorioCommand : EscritorioCommand
    { 
        public RegisterNewEscritorioCommand(string street, string number, string state, int cep, string city, string district)
        {
            Street = street;
            Number = number;
            State = state;
            Cep = cep;
            City = city;
            District = district;
        }
        public override bool IsValid()
        {
            ValidationResult = new RegisterNewEscritorioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
