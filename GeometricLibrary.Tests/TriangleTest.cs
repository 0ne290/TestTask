using GeometricLibrary.Shapes;

namespace GeometricLibrary.Tests;

public class TriangleTest
{
    [Theory]
    [InlineData(4d, 7d, 8d, 13.997768, false)]
    [InlineData(16d, 30d, 34d, 240d, true)]
    public async Task Successful_New(double expectedSide1, double expectedSide2, double expectedSide3, double expectedArea, bool expectedIsRectangular)
    {
        // Act
        var result = await Triangle.New(expectedSide1, expectedSide2, expectedSide3);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedSide1, result.Value.Side1, 6);
        Assert.Equal(expectedSide2, result.Value.Side2, 6);
        Assert.Equal(expectedSide3, result.Value.Side3, 6);
        Assert.Equal(expectedIsRectangular, result.Value.IsRectangular);
        Assert.Equal(expectedArea, result.Value.Area, 6);
    }
    
    [Theory]
    [InlineData(7d, 5d, 2d)]
    [InlineData(5d, 2d, 2d)]
    [InlineData(5d, 7d, 2d)]
    [InlineData(2d, 5d, 2d)]
    [InlineData(2d, 5d, 7d)]
    [InlineData(2d, 2d, 5d)]
    public async Task Failed_New(double side1, double side2, double side3)
    {
        // Act
        var result = await Triangle.New(side1, side2, side3);
        
        // Assert
        Assert.True(result.IsFailed);
    }
}