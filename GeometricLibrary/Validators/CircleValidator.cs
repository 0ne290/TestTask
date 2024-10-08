using FluentValidation;
using GeometricLibrary.Shapes;

namespace GeometricLibrary.Validators;

public class CircleValidator : AbstractValidator<Circle> 
{
    public CircleValidator()
    {
        RuleFor(c => c.Radius).GreaterThan(0);
    }
}