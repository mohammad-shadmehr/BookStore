using BookStore.Application.Validators;

namespace BookStore.Application.Interfaces
{
    public interface IValidator<in T>
    {
        Validation IsValid(T input);
    }
}