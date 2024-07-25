using FluentValidation.Results;

namespace smartfinance.Domain.Common.Extensions
{
    public static class FluentExtensions
    {
        public static IEnumerable<Error> AsReponseErrors(this ValidationResult validationResult) => validationResult.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage));
    }
}
