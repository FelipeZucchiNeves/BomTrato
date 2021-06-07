using BomTratoDomain.Commands.AprovadorCommands;
using FluentValidation;
using System;

namespace BomTratoDomain.Commands.Validations.Aprovador
{
    public abstract class AprovadorValidation<T> : AbstractValidator<T> where T : AprovadorCommand
    {
        public static string NameLasNameErrorMsg => "O campo nome deve ser preenchido";
        public static string NameLastNameLenghtErrorMsg => "O campo nome deve ter pelo menos 2 caracteres e no máximo 50";
        public static string EmailErrorMsg => "O campo email deve ser preenchido";
        public static string EmailInvalidErrorMsg => "O campo nome deve ser válido";
        public static string IdAprovadorErroMsg => "Id do Aprovador inválido";
        public static string BirthDateErroMsg => "O Aprovador deve ter 18 anos";
        protected void ValidateName()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage(NameLasNameErrorMsg)
                .Length(2, 150).WithMessage(NameLastNameLenghtErrorMsg);
        }
        protected void ValidateLastName()
        {
            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage(NameLasNameErrorMsg)
                .Length(2, 150).WithMessage(NameLastNameLenghtErrorMsg);
        }
        protected void ValidateEmail()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(EmailErrorMsg)
                .EmailAddress().WithMessage(EmailInvalidErrorMsg);
        }
        protected void ValidateId()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(IdAprovadorErroMsg);
        }
        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage(BirthDateErroMsg);
        }
        private bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
