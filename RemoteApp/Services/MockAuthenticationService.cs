using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;

namespace RemoteApp.Services;

public sealed record MockUser(string Username);

public sealed class MockAuthenticationService(
    string databasePath,
    IPasswordHasher<MockUser> passwordHasher)
{
    public async Task<bool> AuthenticateAsync(
        string username,
        string password,
        CancellationToken cancellationToken = default)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath
        }.ToString();

        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        await using (var createTable = connection.CreateCommand())
        {
            createTable.CommandText = """
                CREATE TABLE IF NOT EXISTS Users (
                    Username TEXT PRIMARY KEY,
                    PasswordHash TEXT NOT NULL
                );
                """;
            await createTable.ExecuteNonQueryAsync(cancellationToken);
        }

        var demoUser = new MockUser("user");
        string? demoPasswordHash;
        await using (var findDemoUser = connection.CreateCommand())
        {
            findDemoUser.CommandText = "SELECT PasswordHash FROM Users WHERE Username = $username;";
            findDemoUser.Parameters.AddWithValue("$username", demoUser.Username);
            demoPasswordHash = await findDemoUser.ExecuteScalarAsync(cancellationToken) as string;
        }

        if (demoPasswordHash is null)
        {
            await using var seedUser = connection.CreateCommand();
            seedUser.CommandText = """
                INSERT OR IGNORE INTO Users (Username, PasswordHash)
                VALUES ($username, $passwordHash);
                """;
            seedUser.Parameters.AddWithValue("$username", demoUser.Username);
            seedUser.Parameters.AddWithValue(
                "$passwordHash",
                passwordHasher.HashPassword(demoUser, "1234"));
            await seedUser.ExecuteNonQueryAsync(cancellationToken);
        }

        string? passwordHash;
        await using (var findUser = connection.CreateCommand())
        {
            findUser.CommandText = """
                SELECT PasswordHash
                FROM Users
                WHERE Username = $username;
                """;
            findUser.Parameters.AddWithValue("$username", username);
            passwordHash = await findUser.ExecuteScalarAsync(cancellationToken) as string;
        }

        if (passwordHash is null)
            return false;

        var user = new MockUser(username);
        var verification = passwordHasher.VerifyHashedPassword(user, passwordHash, password);
        return verification != PasswordVerificationResult.Failed;
    }
}
