using BomTratoDomain.Commands.EscritorioCommands;

namespace BomTratoDomain.Commands.Validations.Escritorio
{
    public class UpdateEscritorioCommandValidation : EscritorioValidation<UpdateEscritorioCommand>
    {
        public UpdateEscritorioCommandValidation()
        {
            ValidateId();
            ValidateNumber();
            ValidateState();
            ValidateStreet();
            ValidateCity();
            ValidateDistrict();            
        }
    }
}
