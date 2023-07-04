using FluentAssertions;

using Xunit.Abstractions;

namespace CalculatorLibrary.Tests.Unit;

public class CalculatorTests
{
    private readonly Calculator _sut = new ();

    private readonly ITestOutputHelper _outputHelper;

    // Setup goes here in the constructor.
    public CalculatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _outputHelper.WriteLine("CalculatorTests constructor");
    }

    [ Theory, InlineData(5, 4, 9), InlineData(10, 5, 15), InlineData(20, 10, 30) ]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Arrange
        // var calculator = new Calculator();

        // Act
        int result = _sut.Add(a, b);

        // Assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);

        // _outputHelper.WriteLine("Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers");
    }

    [ Theory, InlineData(5, 5,  0), InlineData(15, 5, 10), InlineData(-5, -5, 0), InlineData(-15, -5, -10),
      InlineData(        5, 10, -5) ]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        int result = _sut.Subtract(a, b);

        // Assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);

        // _outputHelper.WriteLine("Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers");
    }

    [ Theory, InlineData(5, 5, 25), InlineData(50, 0, 0), InlineData(-5, 5, -25) ]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        int result = _sut.Multiply(a, b);

        // Assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);
    }

    [ Theory, InlineData(5, 5, 1), InlineData(50, 5, 10), InlineData(-5, 5, -1) ]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreFloats(float a, float b, float expected)
    {
        // Act
        float result = _sut.Divide(a, b);

        // Assert
        // Assert.Equal(expected, result);
        result.Should().Be(expected);
    }

    // Cleanup goes here in the Dispose method.
    // public void Dispose() => _outputHelper.WriteLine("CalculatorTests dispose");

    // Async setup goes here in the InitializeAsync method.
    // public async Task InitializeAsync() => _outputHelper.WriteLine("CalculatorTests initialize");

    // Async cleanup goes here in the DisposeAsync method.
    // public async Task DisposeAsync() => _outputHelper.WriteLine("CalculatorTests dispose");
}
