using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    
    public IEnumerable<CustomerFeedQuery> GetCustomerFeed()
    {
        return _customerRepository.GetCustomerFeed();
    }

    //Create Customer (Maybee validation is needed, check if email exists)
    
    public Customer CreateCustomer(string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        return _customerRepository.CreateCustomer(firstName, lastName, email, address, zip, city, country, phone);
    }
    
    // Update Customer
    
    public Customer UpdateCustomer(int customerId, string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        return _customerRepository.UpdateCustomer(customerId, firstName, lastName, email, address, zip, city, country, phone);
    }
    
    
    // Delete Customer
    
    public void DeleteCustomer(int customerId)
    {
        var result = _customerRepository.DeleteCustomer(customerId);
        if (!result)
        {
            throw new Exception("Unable to delete the Customer");
        }
    }

    
}