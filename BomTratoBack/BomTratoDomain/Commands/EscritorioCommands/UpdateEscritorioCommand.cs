using BomTratoDomain.Commands.Validations.Escritorio;
using System;

namespace BomTratoDomain.Commands.EscritorioCommands
{
    public class UpdateEscritorioCommand : EscritorioCommand
    {
        public UpdateEscritorioCommand(Guid id, string street, string number, string state, int cep, string city, string district)
        {
            Id = id;
            Street = street;
            Number = number;
            State = state;
            Cep = cep;
            City = city;
            District = district;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateEscritorioCommandValidation().Validate(this);
            return base.IsValid();
        }
    }
}
