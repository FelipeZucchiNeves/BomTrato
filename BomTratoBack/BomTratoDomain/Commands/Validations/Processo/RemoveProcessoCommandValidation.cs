using BomTratoDomain.Commands.ProcessoCommands;

namespace BomTratoDomain.Commands.Validations.Processo
{
    public class RemoveProcessoCommandValidation : ProcessoValidation<RemoveProcessoCommand>
    {
        public RemoveProcessoCommandValidation()
        {
            ValidateId();
        }
    }
}
