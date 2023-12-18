using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using infrastructure.DataModels;

namespace api.TransferModels;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class CustomPasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string password = value.ToString();

            // Your custom password validation logic
            const string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""{}|<>]).{8,}$";

            if (!Regex.IsMatch(password, pattern))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid password format.");
            }
        }

        return ValidationResult.Success;
    }
}

public class RegisterUserDto
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "ZIP is required")]
    [Range(1000, 9999, ErrorMessage = "ZIP must be between 1000 and 9999")]
    public int Zip { get; set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Range(10000000, 99999999, ErrorMessage = "Phone must be between 10000000 and 99999999")]
    public int Phone { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [CustomPasswordValidation(ErrorMessage = "Custom Password Validation Failed.")]
    public string Password { get; set; }


}