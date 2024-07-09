using FluentValidation;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;

namespace smartfinance.Domain.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            this.RuleFor(r => r.Name)
                .NotEmpty()
                .WithErrorCode("ACCOUNT-001")
                .WithMessage("O nome deve ser informado.")
                .WithSeverity(Severity.Error);

            this.RuleFor(r => r.Email)
                .EmailAddress()
                .WithErrorCode("ACCOUNT-002")
                .WithMessage("O email é inválido.")
                .WithSeverity(Severity.Error)
                .MaximumLength(450)
                .WithErrorCode("ACCOUNT-003")
                .WithMessage("O email da conta deve conter no máximo 450 caracteres.")
                .WithSeverity(Severity.Error)
                .MustAsync(async (model, email, cancellation) =>
                {
                    bool exists = await _accountRepository.EmailExistsAsync(model.Id, email);
                    return !exists;
                })
                .WithMessage("Email informado já se encontra cadastrado em nossa base de dados.")
                .WithSeverity(Severity.Error);
        }
    }
}
