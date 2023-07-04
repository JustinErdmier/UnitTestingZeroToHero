namespace UnderstandingDependencies.Api.Tests.Unit;

public class FakeUserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult(Enumerable.Empty<User>());
}

public class FakeUserRepositoryWithUsers : IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(new[]
        {
            new User
            {
                Id       = Guid.NewGuid(),
                FullName = "Nick Chapsas"
            }
        });
    }
}
