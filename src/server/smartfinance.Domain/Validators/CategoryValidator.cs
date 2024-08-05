using FluentValidation;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;

namespace smartfinance.Domain.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            this.RuleFor(r => r.Name)
                .NotEmpty()
                .WithErrorCode("CATEGORY-001")
                .WithMessage("O nome deve ser informado.")
                .WithSeverity(Severity.Error)
                .MustAsync(async (model, name, cancellation) =>
                {
                    bool exists = await _categoryRepository.NameExists(model.Id, name);
                    return !exists;
                })
                .WithErrorCode("CATEGORY-002")
                .WithMessage("Nome informado já se encontra cadastrado em nossa base de dados.")
                .WithSeverity(Severity.Error);
        }
    }
}
