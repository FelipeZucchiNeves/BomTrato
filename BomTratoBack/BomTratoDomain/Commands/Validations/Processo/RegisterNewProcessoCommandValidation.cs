using BomTratoDomain.Commands.ProcessoCommands;

namespace BomTratoDomain.Commands.Validations.Processo
{
    public class RegisterNewProcessoCommandValidation : ProcessoValidation<RegisterNewProcessoCommand>
    {
        public  RegisterNewProcessoCommandValidation()
        {
            ValidateProcessNumber();
            ValidateValue();
            ValidateAprovadorId();
            ValidateEscritorioId();
            ValidateComplainerName();
        }
    }
}
