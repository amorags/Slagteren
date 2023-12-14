using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class TokenRepository
{
    
    private readonly NpgsqlDataSource _dataSource;


    public TokenRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public User userFromEmail(string nameClaimValue)
    {
        try
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
        role as {nameof(UserFeedQuery.Role)}
        FROM dinslagter.users
        WHERE Email = @Email";
        
            using (var conn = _dataSource.OpenConnection())
            {
                return conn.QueryFirst<User>(sql, new {Email = nameClaimValue});
            }
        }
        catch (Exception e)
        {
            throw new Exception("User not found");
        }
    }
}