using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class CreateProductRequestDto
{
    [Required(ErrorMessage = "Product Number is required.")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Product Number must be a 6-digit number.")]
    public int ProductNumber { get; set; }

    [Required(ErrorMessage = "Product Name is required.")]
    [MinLength(5, ErrorMessage = "Product Name must be at least 5 characters long.")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public double PricePrKilo { get; set; }

    [Required(ErrorMessage = "Product Type is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid product type.")]
    public int ProductType { get; set; }

    [Required(ErrorMessage = "Country of Birth is required.")]
    [StringLength(50, ErrorMessage = "Country of Birth should be at most 50 characters.")]
    public string CountryOfBirth { get; set; }

    [Required(ErrorMessage = "Production Country is required.")]
    [StringLength(50, ErrorMessage = "Production Country should be at most 50 characters.")]
    public string ProductionCountry { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [MaxLength(500, ErrorMessage = "Description should be at most 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Image URL is required.")]
    [Url(ErrorMessage = "Invalid URL format for Image.")]
    public string ImgUrl { get; set; }

    [Required(ErrorMessage = "Minimum Expiry Date is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum Expiry Date should be a positive number.")]
    public int MinExpDate { get; set; }
}