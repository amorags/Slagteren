using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CustomerRepository
{
    private NpgsqlDataSource _dataSource;

    public CustomerRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
   
    public IEnumerable<CustomerFeedQuery> GetCustomerFeed()
    {
        string sql = $@"
SELECT customer_id as {nameof(CustomerFeedQuery.CustomerId)},
        firstname as {nameof(CustomerFeedQuery.FirstName)},
        lastname as {nameof(CustomerFeedQuery.LastName)},
        email as {nameof(CustomerFeedQuery.Email)},
        address as {nameof(CustomerFeedQuery.address)},
        zip as {nameof(CustomerFeedQuery.Zip)},
        city as {nameof(CustomerFeedQuery.City)},
        country as {nameof(CustomerFeedQuery.Country)},
        phone as {nameof(CustomerFeedQuery.Phone)}
        FROM dinslagter.customers
";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerFeedQuery>(sql);
        }
    }
    
    //Create Customer

    public Customer CreateCustomer(string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        string sql = @"
        INSERT INTO dinslagter.customers (firstName, lastName, email, address, zip, city, country, phone) 
        VALUES (@firstName, @lastName, @email, @address, @zip, @city, @country, @phone)
        RETURNING
        customer_Id as {nameof(customers.Customer_Id)},
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
            return conn.QueryFirst<Customer>(sql, new { firstName, lastName, email, address, zip, city, country, phone });
        }
    }
    
    // Delete Customer
    
    public bool DeleteCustomer(int customerId)
    {
        var sql = @"DELETE FROM dinslagter.customers WHERE customerId = @customer_Id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { customerId }) == 1;
        }
    }
    
    // Update Customer ??
    
    
    public Customer UpdateCustomer(int customer_id, string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        string sql = @"
        UPDATE dinslagter.customers SET firstName = @firstName, lastName = @lastName, email = @email, address = @address, zip = @zip, city = @city, country = @country, phone = @phone
        WHERE customer_id = @customer_id 
        RETURNING
        customer_Id as {nameof(customers.Customer_Id)},
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
            return conn.QueryFirst<Customer>(sql, new { customer_id, firstName, lastName, email, address, zip, city, country, phone });
        }
    }
    
    // Get Customer by ID

    public Customer? GetById(int id)
    {
        string sql = @"
        SELECT
        customer_id as {nameof(customers.Customer_Id)},
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
            return conn.QueryFirst<Customer>(sql, new { id });
        } 
    }

    public bool DoesCustomerWithEmailExist(string email)
    {
        var sql = $@"SELECT COUNT(*) FROM dinslagter.customers WHERE Email = @email;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { email = email}) == 1;
        }
    }
    
}