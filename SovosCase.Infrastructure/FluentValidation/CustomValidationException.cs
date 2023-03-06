using FluentValidation.Results;

namespace SovosCase.Infrastructure.FluentValidation
{
    public class CustomValidationException : Exception
    {
        public IList<string> Failures { get; }

        public CustomValidationException() : base("One or more Validation Failures have occurred.")
        {
            Failures = new List<string>();
        }


        public CustomValidationException(List<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Failures.Add($"{failure.PropertyName}: {failure.ErrorMessage}");
            }
        }
    }
}
