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
    private readonly UserRepository _userRepository;

    public AccountService(ILogger<AccountService> logger, UserRepository userRepository,
        PasswordHashRepository passwordHashRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
    }
    
    // Authenticate Customer.

    public User? Authenticate(string email, string password)
    {
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(email);
            var hashAlgorithm = PasswordHashAlgorithm.Create(passwordHash.Algorithm);
            var isValid = hashAlgorithm.VerifyHashedPassword(password, passwordHash.Hash, passwordHash.Salt);
            if (isValid) return _userRepository.GetById(passwordHash.Customer_Id);
        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        throw new InvalidCredentialException("Invalid credential!");
    }

    public User Register(string firstName, string lastName, string email, string address, int zip, string city, string country, int phone, string password)
    {
        var hashAlgorithm = PasswordHashAlgorithm.Create();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(password, salt);
        var user = _userRepository.CreateUser(firstName, lastName, email, address, zip, city, country, phone);
        _passwordHashRepository.Create(user.UserId, hash, salt, hashAlgorithm.GetName());
        return user;
    }
    
    public User? Get(SessionData data)
    {
        return _userRepository.GetById(data.UserId);
    }
}