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
        string sql = @"
        INSERT INTO dinslagter.users (firstName, lastName, email, address, zip, city, country, phone) 
        VALUES (@firstName, @lastName, @email, @address, @zip, @city, @country, @phone)
        RETURNING
        user_id as {nameof(customers.Customer_Id)},
        firstName as {nameof(customers.FirstName)},
        lastName as {nameof(customers.LastName)},
        email as {nameof(customers.Email)},
        address as {nameof(customers.Address)},
        zip as {nameof(customers.Zip)},
        city as {nameof(customers.City)},
        country as {nameof(customers.Country)},
        phone as {nameof(customers.Phone)}
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { firstName, lastName, email, address, zip, city, country, phone });
        }
    }
    
    // Delete User
    
    public bool DeleteUser(int customerId)
    {
        var sql = @"DELETE FROM dinslagter.users WHERE  = @customer_Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { customerId }) == 1;
        }
    }
    
    // Update User ??
    
    
    public User UpdateUser(int customer_id, string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        string sql = @"
        UPDATE dinslagter.users SET firstName = @firstName, lastName = @lastName, email = @email, address = @address, zip = @zip, city = @city, country = @country, phone = @phone
        WHERE user_id = @customer_id 
        RETURNING
        user_id as {nameof(customers.Customer_Id)},
        firstName as {nameof(customers.FirstName)},
        lastName as {nameof(customers.LastName)},
        email as {nameof(customers.Email)},
        address as {nameof(customers.Address)},
        zip as {nameof(customers.Zip)},
        city as {nameof(customers.City)},
        country as {nameof(customers.Country)},
        phone as {nameof(customers.Phone)}
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { customer_id, firstName, lastName, email, address, zip, city, country, phone });
        }
    }
    
    // Get User by ID

    public User? GetById(int id)
    {
        string sql = @"
        SELECT
        user_id as {nameof(customers.Customer_Id)},
        firstName as {nameof(customers.FirstName)},
        lastName as {nameof(customers.LastName)},
        email as {nameof(customers.Email)},
        address as {nameof(customers.Address)},
        zip as {nameof(customers.Zip)},
        city as {nameof(customers.City)},
        country as {nameof(customers.Country)},
        phone as {nameof(customers.Phone)}
        FROM customers
        WHERE id = customer_id
        ";
            
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { id });
        } 
    }

    public bool DoesUserWithEmailExist(string email)
    {
        var sql = $@"SELECT COUNT(*) FROM dinslagter.users WHERE Email = @email;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { email = email}) == 1;
        }
    }
    
}