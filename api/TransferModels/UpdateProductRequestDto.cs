using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class UpdateProductRequestDto
{
    public int ProductId { get; set; }
    public int ProductNumber { get; set; }
    [MinLength(5)]
    public string ProductName { get; set; }
    public double PricePrKilo { get; set; }
    public int ProductType { get; set; }
    public string CountryOfBirth { get; set; }
    public string ProductionCountry { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public int MinExpDate { get; set; }
}