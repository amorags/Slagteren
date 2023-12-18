using System.Security.Authentication;
using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using service.Password;

namespace service.Services;

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
    
    // Authenticate User.

    public User? Authenticate(string email, string password)
    {
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(email);
            var hashAlgorithm = PasswordHashAlgorithm.Create(passwordHash.Algorithm);
            var isValid = hashAlgorithm.VerifyHashedPassword(password, passwordHash.Hash, passwordHash.Salt);
            if (isValid) return _userRepository.GetById(passwordHash.User_Id);
        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        throw new InvalidCredentialException("Invalid credential!");
    }

    public User Register(string firstName, string lastName, string email, string address, int zip, string city, string country, int phone, string password)
    {
        try
        {
            // Check if the email is unique
            if (!_userRepository.DoesUserWithEmailExist(email))
            {
                // Email is unique, proceed with registration
                var hashAlgorithm = PasswordHashAlgorithm.Create();
                var salt = hashAlgorithm.GenerateSalt();
                var passwordHash = hashAlgorithm.HashPassword(password, salt);
                var user = _userRepository.CreateUser(firstName, lastName, email, address, zip, city, country, phone);
                _passwordHashRepository.Create(user.UserId, passwordHash, salt, hashAlgorithm.GetName());
                return user;
            }
            else
            {
                // Email is not unique, handle accordingly (throw an exception, log, etc.)
                throw new ArgumentException("Email address is already registered.");
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Registration error: {Message}", e);
            throw; // Rethrow the exception to handle it at the controller level
        }
    }
    
    public bool IsEmailInUse(string email)
    {
        return _userRepository.DoesUserWithEmailExist(email);
    }
    

}