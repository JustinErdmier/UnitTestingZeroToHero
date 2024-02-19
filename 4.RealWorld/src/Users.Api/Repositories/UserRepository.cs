﻿using System.Data;

using Dapper;

using Users.Api.Data;
using Users.Api.Models;

namespace Users.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISqliteDbConnectionFactory _connectionFactory;

    public UserRepository(ISqliteDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();

        return await connection.QueryAsync<User>(sql: "select * from Users");
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string        query      = "select * from Users where Id = @Id";
        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
    }

    public async Task<bool> CreateAsync(User user)
    {
        const string        query      = "INSERT INTO Users (Id, FullName) VALUES (@Id, @FullName)";
        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();
        int                 result     = await connection.ExecuteAsync(query, new { user.Id, user.FullName });

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        const string        query      = "DELETE FROM Users where Id = @Id";
        using IDbConnection connection = await _connectionFactory.CreateDbConnectionAsync();
        int                 result     = await connection.ExecuteAsync(query, new { Id = id });

        return result > 0;
    }
}
