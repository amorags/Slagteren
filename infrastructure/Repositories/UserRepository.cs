using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class UserRepository
{
    private NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
   
    public IEnumerable<UserFeedQuery> GetUserFeed()
    {
        string sql = $@"
        SELECT user_id as {nameof(UserFeedQuery.UserId)},
        firstname as {nameof(UserFeedQuery.FirstName)},
        lastname as {nameof(UserFeedQuery.LastName)},
        email as {nameof(UserFeedQuery.Email)},
        address as {nameof(UserFeedQuery.address)},
        zip as {nameof(UserFeedQuery.Zip)},
        city as {nameof(UserFeedQuery.City)},
        country as {nameof(UserFeedQuery.Country)},
        phone as {nameof(UserFeedQuery.Phone)}
        Role as {nameof(UserFeedQuery.Role)}
        FROM dinslagter.users
";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<UserFeedQuery>(sql);
        }
    }
    
    //Create User

    public User CreateUser(string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        string sql = @$"
        INSERT INTO dinslagter.users (firstName, lastName, email, address, zip, city, country, phone) 
        VALUES (@firstName, @lastName, @email, @address, @zip, @city, @country, @phone)
        RETURNING
        user_id as {nameof(User.UserId)},
        firstName as {nameof(User.FirstName)},
        lastName as {nameof(User.LastName)},
        email as {nameof(User.Email)},
        address as {nameof(User.address)},
        zip as {nameof(User.Zip)},
        city as {nameof(User.City)},
        country as {nameof(User.Country)},
        phone as {nameof(User.Phone)}
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { firstName, lastName, email, address, zip, city, country, phone});
        }
    }
    
    // Delete User
    
    public bool DeleteUser(int userId)
    {
        var sql = @"DELETE FROM dinslagter.users WHERE user_id  = @userId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { userId = userId }) == 1;
        }
    }
    
    // Update User ??
    
    
    public User UpdateUser(int userId, string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        string sql = @$"
        UPDATE dinslagter.users SET firstName = @firstName, lastName = @lastName, email = @email, address = @address, zip = @zip, city = @city, country = @country, phone = @phone
        WHERE user_id = @userId 
        RETURNING
        user_id as {nameof(User.UserId)},
        firstName as {nameof(User.FirstName)},
        lastName as {nameof(User.LastName)},
        email as {nameof(User.Email)},
        address as {nameof(User.address)},
        zip as {nameof(User.Zip)},
        city as {nameof(User.City)},
        country as {nameof(User.Country)},
        phone as {nameof(User.Phone)}
        role as {nameof(User.Role)}
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { userId, firstName, lastName, email, address, zip, city, country, phone });
        }
    }
    
    // Get User by ID

    public User? GetById(int userId)
    {
        string sql = @$"
        SELECT
        user_id as {nameof(UserFeedQuery.UserId)},
        firstName as {nameof(UserFeedQuery.FirstName)},
        lastName as {nameof(UserFeedQuery.LastName)},
        email as {nameof(UserFeedQuery.Email)},
        address as {nameof(UserFeedQuery.address)},
        zip as {nameof(UserFeedQuery.Zip)},
        city as {nameof(UserFeedQuery.City)},
        country as {nameof(UserFeedQuery.Country)},
        phone as {nameof(UserFeedQuery.Phone)}
        role as {nameof(UserFeedQuery.Role)}
        FROM dinslagter.users
        WHERE user_id = @userId 
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { userId });
        } 
    }

    public bool DoesUserWithEmailExist(string email)
    {
        var sql = $@"SELECT COUNT(*) FROM dinslagter.users WHERE Email = @email;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { email }) == 1;
        }
    }
    
}