using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class RegisterCustomerDto
{
    [Required] public required string FirstName { get; set; }

    [Required] public required string LastName { get; set; }
    
    [Required] public required string Email { get; set; }
    
    [Required] public required string Address { get; set; }
    
    [Required] public required int Zip { get; set; }
    
    [Required] public required string City { get; set; }
    
    [Required] public required string Country { get; set; }
    
    [Required] public required int Phone { get; set; }

    [Required] [MinLength(8)] public required string Password { get; set; }
    
}