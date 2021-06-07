using BomTratoDomain.Commands.EscritorioCommands;
using FluentValidation;
using System;

namespace BomTratoDomain.Commands.Validations.Escritorio
{
    public abstract class EscritorioValidation <T> : AbstractValidator<T> where T : EscritorioCommand
    {
        public static string StreetErrorMsg => "O campo rua deve ser preenchido";
        public static string StreetLengthErrorMsg => "O campo Rua deve ter no mínimo 5 caracteres e no máximo 100";
        public static string StateLengthErrorMsg => "O campo Rua deve ter no mínimo 1 caracteres e no máximo 4";
        public static string StateErrorMsg => "O campo estado deve ser preenchido";
        public static string NumberErrorMsg => "O número deve ser preenchido";
        public static string NumberLengthErrorMsg => "O campo numero deve ter no mínimo 1 caracteres e no máximo 5";
        public static string IdEscritorioErroMsg => "Id Aprovador inválido";
        public static string CityErrorMsg => "O campo cidade deve ser preenchido";
        public static string CityLengthErrorMsg => "O campo cidade deve ter no mínimo 2 caracteres e no máximo 50";
        public static string DistrictErrorMsg => "O campo bairro deve ser preenchido";
        public static string DistrictLengthErrorMsg => "O campo bairro deve ter no mínimo 2 caracteres e no máximo 50";
        protected void ValidateId()
        {
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty)
                .WithMessage(IdEscritorioErroMsg);
        }
        protected void ValidateStreet()
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage(StreetErrorMsg)
                .Length(2, 100).WithMessage(StreetLengthErrorMsg);
        }
        protected void ValidateNumber()
        {
            RuleFor(a => a.Number)
                .NotEmpty().WithMessage(NumberErrorMsg)
                .Length(1, 5).WithMessage(NumberLengthErrorMsg);
        }
        protected void ValidateState()
        {
            RuleFor(a => a.State)
                .NotEmpty().WithMessage(StateErrorMsg)
                .Length(1, 4).WithMessage(StateLengthErrorMsg);
        }
        protected void ValidateCity()
        {
            RuleFor(a => a.City)
                .NotEmpty().WithMessage(CityErrorMsg)
                .Length(2, 50).WithMessage(CityLengthErrorMsg);
        }
        protected void ValidateDistrict()
        {
            RuleFor(a => a.District)
                .NotEmpty().WithMessage(DistrictErrorMsg)
                .Length(2, 50).WithMessage(DistrictLengthErrorMsg);
        }
    }
}
