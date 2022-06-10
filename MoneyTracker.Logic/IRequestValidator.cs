namespace MoneyTracker.Logic;

public interface IRequestValidator<in TRequest> where TRequest : class
{
    Task ValidateAsync(TRequest request);
}