using FluentResults;
using GeometricLibrary.Validators;

namespace GeometricLibrary.Shapes;

public class Circle : IShape
{
    private Circle(double radius)
    {
        Radius = radius;
        Area = Math.PI * Radius * Radius;
    }
    
    public static async Task<Result<Circle>> New(double radius)
    {
        var circle = new Circle(radius);
        var circleValidator = new CircleValidator();
        var validationResult = await circleValidator.ValidateAsync(circle);

        return validationResult.IsValid ? Result.Ok(circle) : Result.Fail(validationResult.ToString("; "));
    }
    
    public double Area { get; }

    public double Radius { get; }
}