using System.Data;

using Dapper;

using Users.Api.Models;

namespace Users.Api.Data;

public class DatabaseInitializer
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(ISqliteDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task InitializeAsync()
    {
        SqlMapper.AddTypeHandler(new SqLiteGuidTypeHandler());
        SqlMapper.RemoveTypeMap(typeof(Guid));
        SqlMapper.RemoveTypeMap(typeof(Guid?));

        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();
        await connection.ExecuteAsync(sql: "CREATE TABLE IF NOT EXISTS Users (Id TEXT PRIMARY KEY, FullName TEXT NOT NULL)");

        User? nickChapsas =
            await connection.QuerySingleOrDefaultAsync<User>(sql: "SELECT * FROM Users where FullName = @FullName",
                                                             new { FullName = "Nick Chapsas" });

        if (nickChapsas is null)
            await connection.ExecuteAsync(sql: "INSERT INTO Users (Id, FullName) VALUES (@Id, @FullName)",
                                          new
                                          {
                                              Id = Guid.NewGuid()
                                                       .ToString(),
                                              FullName = "Nick Chapsas"
                                          });
    }
}
