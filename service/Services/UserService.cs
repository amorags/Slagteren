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
    
    
    public IEnumerable<UserFeedQuery> GetUserFeed()
    {
        return _userRepository.GetUserFeed();
    }

    //Create Customer (Maybee validation is needed, check if email exists)
    
    public User CreateUser(string firstName, string lastName, string email, string address, int zip,
        string city, string country, int phone)
    {
        return _userRepository.CreateUser(firstName, lastName, email, address, zip, city, country, phone);
    }
    
    // Update Customer
    
    public User UpdateUser(int customerId, string firstName, string lastName, string email, string address, int zip,
        string city, string country, string phone)
    {
        return _userRepository.UpdateUser(customerId, firstName, lastName, email, address, zip, city, country, phone);
    }
    
    
    // Delete Customer
    
    public void DeleteUser(int customerId)
    {
        var result = _userRepository.DeleteUser(customerId);
        if (!result)
        {
            throw new Exception("Unable to delete the Customer");
        }
    }

    
}