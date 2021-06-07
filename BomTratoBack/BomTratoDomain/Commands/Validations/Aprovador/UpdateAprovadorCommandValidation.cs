using BomTratoDomain.Commands.AprovadorCommands;

namespace BomTratoDomain.Commands.Validations.Aprovador
{
    public class UpdateAprovadorCommandValidation : AprovadorValidation<UpdateAprovadorCommand>
    {
        public UpdateAprovadorCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidateBirthDate();
        }
    }
}
