using FluentValidation;
using TodoXpress.Domain.Common.ValueTypes;

namespace TodoXpress.Infastructure.Validation.Common;

/// <summary>
/// Validator for the <see cref="ARGBColor"/> class.
/// </summary>
public class ARGBColorValidator : AbstractValidator<ARGBColor>
{
    public ARGBColorValidator()
    {
        RuleFor(c => c.A).InclusiveBetween(0,1)
            .WithMessage("Invalid alpha value");
        RuleFor(c => c.R).InclusiveBetween(0,255)
            .WithMessage("Invalid value for red");
        RuleFor(c => c.G).InclusiveBetween(0,255)
            .WithMessage("Invalid value for green");
        RuleFor(c => c.B).InclusiveBetween(0,255)
            .WithMessage("Invalid value for blue");
    }
}

public class NullableARGBColorValidator : AbstractValidator<ARGBColor?>
{
    public NullableARGBColorValidator()
    {
        When(x => x is not null, () => {
            RuleFor(x => x!.Value)
                .SetValidator(new ARGBColorValidator());
        });
    }
}