using FluentValidation;
using GeometricLibrary.Shapes;

namespace GeometricLibrary.Validators;

public class TriangleValidator : AbstractValidator<Triangle> 
{
    public TriangleValidator()
    {
        RuleFor(t => t.Side1).GreaterThan(0).LessThan(t => t.Side2 + t.Side3);
        RuleFor(t => t.Side2).GreaterThan(0).LessThan(t => t.Side1 + t.Side3);
        RuleFor(t => t.Side3).GreaterThan(0).LessThan(t => t.Side1 + t.Side2);
    }
}