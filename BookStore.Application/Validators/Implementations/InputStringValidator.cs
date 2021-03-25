
using BookStore.Application.Interfaces;

namespace BookStore.Application.Validators.Implementations
{
    public class InputStringValidator : IValidator<string>
    {
        public Validation IsValid(string input)
        {
            var result = new Validation() { IsValid = true };

            if (string.IsNullOrEmpty(input))
            {
                result.IsValid = false;
                result.ErrorMessage = "Input cannot be empty";
            }

            return result;
        }
    }
}
