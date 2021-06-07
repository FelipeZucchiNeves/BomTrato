using BomTratoDomain.Commands.AprovadorCommands;

namespace BomTratoDomain.Commands.Validations.Aprovador
{
    public class RemoveAprovadorCommandValidation : AprovadorValidation<RemoveAprovadorCommand>
    {
        public RemoveAprovadorCommandValidation()
        {
            ValidateId();
        }
    }
}
