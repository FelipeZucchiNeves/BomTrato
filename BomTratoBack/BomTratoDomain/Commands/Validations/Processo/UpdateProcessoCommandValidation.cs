using BomTratoDomain.Commands.ProcessoCommands;

namespace BomTratoDomain.Commands.Validations.Processo
{
    public class UpdateProcessoCommandValidation : ProcessoValidation<UpdateProcessoCommand>
    {
        public UpdateProcessoCommandValidation()
        {
            ValidateId();
            ValidateProcessNumber();
            ValidateValue();
            ValidateAprovadorId();
            ValidateEscritorioId();
            ValidateComplainerName();
            
        }
    }
}
