using BomTratoDomain.Commands.AprovadorCommands;

namespace BomTratoDomain.Commands.Validations.Aprovador
{
    public class RegisterNewAprovadorCommandValidation : AprovadorValidation<RegisterNewAprovadorCommand>
    {
        public  RegisterNewAprovadorCommandValidation()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidateBirthDate();
        }
    }
}
