using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OnlineShop.Exceptions;

namespace OnlineShop.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult=await validators.First().ValidateAsync(context, cancellationToken);
            if (!validationResult.IsValid)
            {
                var failures = Serialize(validationResult.Errors);
                throw new BadRequestException("Input is not incorrect",failures);
            }
        }
        return await next(cancellationToken);
    }

    public static Dictionary<string, string[]> Serialize(IEnumerable<ValidationFailure> failures)
    {
        var errors = failures.GroupBy(failures => failures.PropertyName).
            ToDictionary
            (
            group => group.Key,
            group => group.Select(failures => failures.ErrorMessage).ToArray()
            );
        return errors;
    }
}
