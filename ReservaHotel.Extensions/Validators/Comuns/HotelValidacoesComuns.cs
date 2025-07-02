using FluentValidation;

namespace ReservaHotel.Extensions.Validators.Comuns
{
    public static class HotelValidacoesComuns
    {
        public static IRuleBuilderOptions<T,string> NomeObrigatorio<T>
            (this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().WithMessage("É necessário informar o nome do hotel").MinimumLength(5).WithMessage("É necessário informar o nome do hotel").MaximumLength(150).WithMessage("É necessário informar o nome do hotel");
        }
        public static IRuleBuilderOptions<T, int> NumeroDeEstrelas<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.NotNull().GreaterThanOrEqualTo(1).WithMessage("O Hotel deve possuir ao menos uma estrela").
                LessThanOrEqualTo(5).WithMessage("O Hotel deve possuir no máximo 5 estrelas");
        }
        public static IRuleBuilderOptions<T, int> NumeroDeAndares<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.NotNull().GreaterThanOrEqualTo(0).WithMessage("Número de andares do Hotel é inválido");
        }
    }
}
