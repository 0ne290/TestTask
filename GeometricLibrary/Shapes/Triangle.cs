using FluentResults;
using GeometricLibrary.Validators;

namespace GeometricLibrary.Shapes;

public class Triangle : IShape
{
    private Triangle(double side1, double side2, double side3)
    {
        Side1 = side1;
        Side2 = side2;
        Side3 = side3;
        var semiperimeter = (Side1 + Side2 + Side3) / 2;
        Area = Math.Sqrt(semiperimeter * (semiperimeter - Side1) * (semiperimeter - Side2) *
                         (semiperimeter - Side3));
        var sidesByDescOfLength = new[] { side1, side2, side3 };
        Array.Sort(sidesByDescOfLength, (s1, s2) => s2.CompareTo(s1));
        IsRectangular = Math.Abs(sidesByDescOfLength[0] * sidesByDescOfLength[0] - (sidesByDescOfLength[1] * sidesByDescOfLength[1] +
            sidesByDescOfLength[2] * sidesByDescOfLength[2])) < 0.0000001;
    }
    
    public static async Task<Result<Triangle>> New(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        var triangleValidator = new TriangleValidator();
        var validationResult = await triangleValidator.ValidateAsync(triangle);

        return validationResult.IsValid ? Result.Ok(triangle) : Result.Fail(validationResult.ToString("; "));
    }
    
    public double Area { get; }
    
    public bool IsRectangular { get; }

    public double Side1 { get; }
    
    public double Side2 { get; }
    
    public double Side3 { get; }
}