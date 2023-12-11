using System.Security.Authentication;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using service.Password;

namespace service;

public class AccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly PasswordHashRepository _passwordHashRepository;
    private readonly CustomerRepository _customerRepository;

    public AccountService(ILogger<AccountService> logger, CustomerRepository customerRepository,
        PasswordHashRepository passwordHashRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _passwordHashRepository = passwordHashRepository;
    }
    
    // Authenticate Customer.

    public Customer? Authenticate(string email, string password)
    {
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(email);
            var hashAlgorithm = PasswordHashAlgorithm.Create(passwordHash.Algorithm);
            var isValid = hashAlgorithm.VerifyHashedPassword(password, passwordHash.Hash, passwordHash.Salt);
            if (isValid) return _customerRepository.GetById(passwordHash.Customer_Id);
        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        throw new InvalidCredentialException("Invalid credential!");
    }

    public Customer Register(string firstName, string lastName, string email, string address, int zip, string city, string country, string phone, string password)
    {
        var hashAlgorithm = PasswordHashAlgorithm.Create();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(password, salt);
        var customer = _customerRepository.CreateCustomer(firstName, lastName, email, address, zip, city, country, phone);
        _passwordHashRepository.Create(customer.Customer_Id, hash, salt, hashAlgorithm.GetName());
        return customer;
    }
}