using GeometricLibrary.Shapes;

namespace Console;

internal static class Program
{
    private static async Task Main()
    {
        var result = await ShapeFromInput();
        if (result.IsSuccess)
        {
            if (result.Shape is Circle circle)
                System.Console.WriteLine($"Circle attributes:\n\tRadius: {circle.Radius}\n\tArea: {circle.Area}");
            else if (result.Shape is Triangle triangle)
                System.Console.WriteLine(
                    $"Triangle attributes:\n\tSide1: {triangle.Side1}\n\tSide2: {triangle.Side2}\n\tSide3: {triangle.Side3}\n\tArea: {triangle.Area}\n\tIsRectangular: {triangle.IsRectangular}");
        }

        System.Console.Write("Press any key to terminate the program...");
        System.Console.ReadKey();
        await Task.CompletedTask;
    }

    private static async Task<(bool IsSuccess, IShape? Shape)> ShapeFromInput()
    {
        while (true)
        {
            System.Console.Write("Type of shape (t - triangle, c - circle, e - exit): ");
            var typeShape = System.Console.ReadLine();

            if (typeShape == "t")
            {
                System.Console.Write("\tThree sides of a triangle separated by a space: ");
                var result = await TryDeserializeTriangle(System.Console.ReadLine());

                if (result.IsSuccess)
                    return (true, result.Value);

                System.Console.WriteLine("\t\t" + result.ErrorMessage);
                continue;
            }

            if (typeShape == "c")
            {
                System.Console.Write("\tRadius of a circle: ");
                var result = await TryDeserializeCircle(System.Console.ReadLine());

                if (result.IsSuccess)
                    return (true, result.Value);

                System.Console.WriteLine("\t\t" + result.ErrorMessage);
                continue;
            }

            if (typeShape == "e")
                return (true, null);
        }
    }
    
    private static async Task<(bool IsSuccess, string ErrorMessage, Triangle? Value)> TryDeserializeTriangle(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return (false, "Failed to create triangle. Error messages: invalid input format.", null);

        var stringsOfSides = value.Split(" ");
        if (stringsOfSides.Length != 3)
            return (false, "Failed to create triangle. Error messages: invalid input format.", null);

        if (!(double.TryParse(stringsOfSides[0], out var side1) &&
              double.TryParse(stringsOfSides[1], out var side2) &&
              double.TryParse(stringsOfSides[2], out var side3)))
            return (false, "Failed to create triangle. Error messages: invalid input format.", null);

        var result = await Triangle.New(side1, side2, side3);

        return result.IsFailed
            ? (false,
                $"Failed to create triangle. Error messages: {result.Reasons.Select(r => r.Message).Aggregate((m1, m2) => m1 + "; " + m2)}",
                null)
            : (true, string.Empty, result.Value);
    }
    
    private static async Task<(bool IsSuccess, string ErrorMessage, Circle? Value)> TryDeserializeCircle(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return (false, "Failed to create circle. Error messages: invalid input format.", null);

        if (!double.TryParse(value, out var radius))
            return (false, "Failed to create circle. Error messages: invalid input format.", null);

        var result = await Circle.New(radius);

        return result.IsFailed
            ? (false,
                $"Failed to create circle. Error messages: {result.Reasons.Select(r => r.Message).Aggregate((m1, m2) => m1 + "; " + m2)}",
                null)
            : (true, string.Empty, result.Value);
    }
}
