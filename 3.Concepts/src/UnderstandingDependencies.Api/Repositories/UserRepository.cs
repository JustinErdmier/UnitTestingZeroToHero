namespace UnderstandingDependencies.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqliteDbConnectionFactory _connectionFactory;

    public UserRepository() => _connectionFactory = new SqliteDbConnectionFactory();

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();

        return await connection.QueryAsync<User>("select * from Users");
    }
}
