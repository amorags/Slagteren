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
    
    public void Create(int customer_id, string hash, string salt, string algorithm)
    {
        const string sql = $@"
    INSERT INTO password_hash (customer_id, hash, salt, algorithm)
    VALUES (@userId, @hash, @salt, @algorithm)
";
        using (var conn = _dataSource.OpenConnection())
        { 
            conn.Execute(sql, new { customer_id, hash, salt, algorithm });
        }
        
    }

    
    
    //Retrieve by email
    
    public PasswordHash GetByEmail(string email)
    {
        const string sql = $@"
    SELECT
        customer_id as {nameof(PasswordHash.Customer_Id)},
        hash as {nameof(PasswordHash.Hash)},
        salt as {nameof(PasswordHash.Salt)},
        algorithm as {nameof(PasswordHash.Algorithm)}
    FROM password_hash
    JOIN customers ON password_hash.customer_id = customer_id
    WHERE email = @email;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<PasswordHash>(sql, new { email });
        }
    }
    
    
    
    //Update
    
    public void Update(int customer_id, string hash, string salt, string algorithm)
    {
        const string sql = $@"
UPDATE password_hash
SET hash = @hash, salt = @salt, algorithm = @algorithm
WHERE customer_id = @customer_Id
";
        using (var conn = _dataSource.OpenConnection())
        { 
            conn.Execute(sql, new { customer_id, hash, salt, algorithm });
        }
    }


}