using GeometricLibrary.Shapes;

namespace GeometricLibrary.Tests;

public class CircleTest
{
    [Fact]
    public async Task Successful_New()
    {
        // Arrange
        const double expectedRadius = 11d;
        const double expectedArea = 380.132711;
        
        // Act
        var result = await Circle.New(expectedRadius);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedRadius, result.Value.Radius, 6);
        Assert.Equal(expectedArea, result.Value.Area, 6);
    }
    
    [Theory]
    [InlineData(0d)]
    [InlineData(-11d)]
    public async Task Failed_New(double radius)
    {
        // Act
        var result = await Circle.New(radius);
        
        // Assert
        Assert.True(result.IsFailed);
    }
}