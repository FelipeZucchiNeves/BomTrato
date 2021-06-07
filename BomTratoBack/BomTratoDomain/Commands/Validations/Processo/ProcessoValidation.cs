using BomTratoDomain.Commands.ProcessoCommands;
using FluentValidation;
using System;

namespace BomTratoDomain.Commands.Validations.Processo
{
    public abstract class ProcessoValidation<T> : AbstractValidator<T> where T : ProcessoCommand
    {
        public static string ComplainerNameErrorMsg => "O campo nome cliente deve ser preenchido";
        public static string ComplainerNameLenghtErrorMsg => "O campo Rua deve ter no mínimo 1 caracteres e no máximo 250";
        public static string EscritorioErrorMsg => "O campo escritório deve ser preenchido";
        public static string AprovadorErrorMsg => "O campo aprovador deve ser preenchido";
        public static string ProcessNumberErrorMsg => "O campo numero do processo deve ter 12 caracteres";
        public static string ValueErroMsg => "O valor deve ser superior a R$30.000,00";
        public static string IdErrorMsg => "Id Processo invalido";
        protected void ValidateProcessNumber()
        {
            RuleFor(a => a.ProcessNumber)
                .Length(12)
                .WithMessage(ProcessNumberErrorMsg);
        }
        protected void ValidateValue()
        {
            RuleFor(a => a.Value)
                .GreaterThanOrEqualTo(30000)
                .WithMessage(ValueErroMsg);
        }
        protected void ValidateAprovadorId()
        {
            RuleFor(a => a.AprovadorId)
                .NotEqual(Guid.Empty)
                .WithMessage(AprovadorErrorMsg);
        }
        protected void ValidateEscritorioId()
        {
            RuleFor(a => a.EscritorioId)
                .NotEqual(Guid.Empty)
                .WithMessage(EscritorioErrorMsg);
        }
        protected void ValidateComplainerName()
        {
            RuleFor(p => p.ComplainerName)
                .NotEmpty().WithMessage(ComplainerNameErrorMsg)
                .Length(2, 150).WithMessage(ComplainerNameLenghtErrorMsg);
        }
        protected void ValidateId()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(IdErrorMsg);
        }
    }
}
