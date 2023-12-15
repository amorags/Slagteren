using System.Security.Cryptography;
using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;


public class PasswordHashRepository
{
    
    private NpgsqlDataSource _dataSource;

    public PasswordHashRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    
    
    //Create
    
    public void Create(int user_id, string password_hash, string salt, string algorithm)
    {
        const string sql = $@"
    INSERT INTO passwordhash (user_id, password_hash, salt, algorithm)
    VALUES (@user_id, @password_hash, @salt, @algorithm)
    ";
        // Log or print the SQL query (for debugging purposes)
        Console.WriteLine($"Executing SQL Query: {sql}");
        
        using (var conn = _dataSource.OpenConnection())
        { 
            conn.Execute(sql, new { user_id, password_hash, salt, algorithm });
        }
        
    }

    
    
    //Retrieve by email
    
    public PasswordHash GetByEmail(string email)
    {
        const string sql = $@"
    SELECT
        user_id as {nameof(PasswordHash.User_Id)},
        hash as {nameof(PasswordHash.Hash)},
        salt as {nameof(PasswordHash.Salt)},
        algorithm as {nameof(PasswordHash.Algorithm)}
    FROM password_hash
    JOIN users ON password_hash.user_id = user_id
    WHERE email = @email;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<PasswordHash>(sql, new { email });
        }
    }
    
    
    
    //Update
    
    public void Update(int user_id, string hash, string salt, string algorithm)
    {
        const string sql = $@"
UPDATE password_hash
SET hash = @hash, salt = @salt, algorithm = @algorithm
WHERE user_id = @user_Id
";
        using (var conn = _dataSource.OpenConnection())
        { 
            conn.Execute(sql, new { user_id, hash, salt, algorithm });
        }
    }


}