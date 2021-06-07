using BomTratoDomain.Commands.EscritorioCommands;

namespace BomTratoDomain.Commands.Validations.Escritorio
{
    public class RemoveEscritorioCommandValidation : EscritorioValidation<RemoveEscritorioCommand>
    {
        public RemoveEscritorioCommandValidation()
        {
            ValidateId();
        }
    }
}
