using BomTratoDomain.Commands.EscritorioCommands;

namespace BomTratoDomain.Commands.Validations.Escritorio
{
    public class RegisterNewEscritorioCommandValidation : EscritorioValidation<RegisterNewEscritorioCommand>
    {
        public RegisterNewEscritorioCommandValidation()
        {
            ValidateStreet();
            ValidateNumber();
            ValidateState();
            ValidateCity();
            ValidateDistrict();
        }
    }
}
