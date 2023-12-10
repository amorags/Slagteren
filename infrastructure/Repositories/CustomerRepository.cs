using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CustomerRepository
{
    private NpgsqlDataSource _dataSource;

    public CustomerRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
   
    //Create Customer

    public Customer CreateCustomer(string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        string sql = @"
        INSERT INTO DinSlagter.customers (firstName, lastName, email, address, zip, city, country, phone) 
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
        var sql = @"DELETE FROM DinSlagter.customers WHERE customerId = @customer_Id;";
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
        UPDATE DinSlagter.customers SET firstName = @firstName, lastName = @lastName, email = @email, address = @address, zip = @zip, city = @city, country = @country, phone = @phone
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
}