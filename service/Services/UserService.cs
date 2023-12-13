using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    //Get user for admin list
    
    public IEnumerable<UserFeedQuery> GetUserFeed()
    {
        return _userRepository.GetUserFeed();
    }

    //Create user and validation Email
    
    public User CreateUser(string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        var doesEmailExist = _userRepository.DoesUserWithEmailExist(email);
        if (!doesEmailExist)
        {
            throw new ValidationException("a user with this email already exists " + email);
        }
        
        return _userRepository.CreateUser(firstName, lastName, email, address, zip, city, country, phone);
    }
    
    // Update user
    
    public User UpdateUser(int customerId, string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        return _userRepository.UpdateUser(customerId, firstName, lastName, email, address, zip, city, country, phone);
    }
    
    // Delete user
    
    public void DeleteUser(int userId)
    {
        var result = _userRepository.DeleteUser(userId);
        if (!result)
        {
            throw new Exception("Unable to delete the user");
        }
    }

    
}