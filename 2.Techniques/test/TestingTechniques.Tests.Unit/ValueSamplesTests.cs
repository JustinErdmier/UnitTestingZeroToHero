using FluentAssertions.Events;

namespace TestingTechniques.Tests.Unit;

public class ValueSamplesTests
{
    #region MyCode

    private readonly ValueSamples _sut = new ();

    [ Fact ]
    public void StringAssertionExample()
    {
        string fullName = _sut.FullName;

        fullName.Should().Be("Nick Chapsas");

        fullName.Should().NotBeEmpty();

        fullName.Should().StartWith("Nick");

        fullName.Should().EndWith("Chapsas");
    }

    [ Fact ]
    public void NumberAssertionExample()
    {
        int age = _sut.Age;

        age.Should().Be(21);

        age.Should().BePositive();

        age.Should().BeGreaterThan(20);

        age.Should().BeLessOrEqualTo(21);

        age.Should().BeInRange(18, 60);
    }

    [ Fact ]
    public void DateAssertionExample()
    {
        DateOnly dateOfBirth = _sut.DateOfBirth;

        dateOfBirth.Should().Be(new DateOnly(2000, 6, 9));

        dateOfBirth.Should().BeInRange(new DateOnly(2000, 1, 1), new DateOnly(2001, 1, 1));

        dateOfBirth.Should().BeGreaterThan(new DateOnly(2000, 1, 1));
    }

    [ Fact ]
    public void ObjectAssertionExample()
    {
        User expected = new ()
        {
            FullName    = "Nick Chapsas",
            Age         = 21,
            DateOfBirth = new DateOnly(2000, 6, 9)
        };

        User user = _sut.AppUser;

        // These assertions compare the equality of the objects' reference, not their content.
        //Assert.Equal(expected, user);
        //user.Should().Be(expected);

        // This assertion compares the equality of the objects' content, not their reference.
        user.Should().BeEquivalentTo(expected);
    }

    [ Fact ]
    public void EnumerableObjectsAssertionExample()
    {
        User expected = new ()
        {
            FullName    = "Nick Chapsas",
            Age         = 21,
            DateOfBirth = new DateOnly(2000, 6, 9)
        };

        IEnumerable<User> users = _sut.Users.As<User[]>();

        users.Should().ContainEquivalentOf(expected);

        users.Should().HaveCount(3);

        users.Should().Contain(x => x.FullName.StartsWith("Nick") && x.Age > 5);
    }

    [ Fact ]
    public void EnumerableNumbersAssertionExample()
    {
        IEnumerable<int> numbers = _sut.Numbers.As<int[]>();

        numbers.Should().Contain(5);

        numbers.Should().HaveCount(4);

        numbers.Should().Contain(x => x > 5);
    }

    [ Fact ]
    public void ExceptionThrownAssertionExample()
    {
        Calculator calculator = new ();

        // We're using the type Action because we don't actually care about the return value of the method. If we did, we would use Func<float>.
        Action result = () => calculator.Divide(1, 0);

        result.Should()
              .Throw<DivideByZeroException>()
              .WithMessage("Attempted to divide by zero.");
    }

    [ Fact ]
    public void EventRaisedAssertionExample()
    {
        IMonitor<ValueSamples> monitorSubject = _sut.Monitor();

        _sut.RaiseExampleEvent();

        monitorSubject.Should().Raise(nameof(ValueSamples.ExampleEvent));
    }

    [ Fact ]
    public void TestingInternalMembersExample()
    {
        // This internal member is visible because the source assembly's .csproj file exposes internals to the test assembly using the InternalsVisibleTo tag.
        int number = _sut.InternalSecretNumber;

        number.Should().Be(42);
    }

    #endregion

    #region CourseSourceCode

    // private readonly ValueSamples _sut = new ();
    //
    // [ Fact ]
    // public void StringAssertionExample()
    // {
    //     string fullName = _sut.FullName;
    //
    //     fullName.Should().Be("Nick Chapsas");
    //     fullName.Should().NotBeEmpty();
    //     fullName.Should().StartWith("Nick");
    //     fullName.Should().EndWith("Chapsas");
    // }
    //
    // [ Fact ]
    // public void NumberAssertionExample()
    // {
    //     int age = _sut.Age;
    //
    //     age.Should().Be(21);
    //     age.Should().BePositive();
    //     age.Should().BeGreaterThan(20);
    //     age.Should().BeLessOrEqualTo(21);
    //     age.Should().BeInRange(18, 60);
    // }
    //
    // [ Fact ]
    // public void DateAssertionExample()
    // {
    //     DateOnly dateOfBirth = _sut.DateOfBirth;
    //
    //     dateOfBirth.Should().Be(new DateOnly(2000,            6, 9));
    //     dateOfBirth.Should().BeInRange(new DateOnly(2000,     1, 1), new DateOnly(2001, 1, 1));
    //     dateOfBirth.Should().BeGreaterThan(new DateOnly(2000, 1, 1));
    // }
    //
    // [ Fact ]
    // public void ObjectAssertionExample()
    // {
    //     var expected = new User
    //     {
    //         FullName    = "Nick Chapsas",
    //         Age         = 21,
    //         DateOfBirth = new DateOnly(2000, 6, 9)
    //     };
    //
    //     User user = _sut.AppUser;
    //
    //     //Assert.Equal(expected, user);
    //     //user.Should().Be(expected);
    //     user.Should().BeEquivalentTo(expected);
    // }
    //
    // [ Fact ]
    // public void EnumerableObjectsAssertionExample()
    // {
    //     var expected = new User
    //     {
    //         FullName    = "Nick Chapsas",
    //         Age         = 21,
    //         DateOfBirth = new DateOnly(2000, 6, 9)
    //     };
    //
    //     User[]? users = _sut.Users.As<User[]>();
    //
    //     users.Should().ContainEquivalentOf(expected);
    //     users.Should().HaveCount(3);
    //     users.Should().Contain(x => x.FullName.StartsWith("Nick") && x.Age > 5);
    // }
    //
    // [ Fact ]
    // public void EnumerableNumbersAssertionExample()
    // {
    //     int[]? numbers = _sut.Numbers.As<int[]>();
    //
    //     numbers.Should().Contain(5);
    // }
    //
    // [ Fact ]
    // public void ExceptionThrownAssertionExample()
    // {
    //     var calculator = new Calculator();
    //
    //     Action result = () => calculator.Divide(1, 0);
    //
    //     result.Should()
    //           .Throw<DivideByZeroException>()
    //           .WithMessage("Attempted to divide by zero.");
    // }
    //
    // [ Fact ]
    // public void EventRaisedAssertionExample()
    // {
    //     IMonitor<ValueSamples>? monitorSubject = _sut.Monitor();
    //
    //     _sut.RaiseExampleEvent();
    //
    //     monitorSubject.Should().Raise("ExampleEvent");
    // }
    //
    // [ Fact ]
    // public void TestingInternalMembersExample()
    // {
    //     int number = _sut.InternalSecretNumber;
    //
    //     number.Should().Be(42);
    // }

    #endregion CourseSourceCode
}
