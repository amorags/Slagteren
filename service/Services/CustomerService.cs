using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    //Create Customer (Maybee validation is needed, check if email exists)
    
    public Customer CreateCustomer(string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        return _customerRepository.CreateCustomer(firstName, lastName, email, address, zip, city, country, phone);
    }
    
    // Update Customer
    
    public Customer UpdateCustomer(int customer_id, string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        return _customerRepository.UpdateCustomer(customer_id, firstName, lastName, email, address, zip, city, country, phone);
    }
    
    
    // Delete Customer
    
    public void DeleteCustomer(int customer_id)
    {
        var result = _customerRepository.DeleteCustomer(customer_id);
        if (!result)
        {
            throw new Exception("Unable to delete the Customer");
        }
    }
    
}