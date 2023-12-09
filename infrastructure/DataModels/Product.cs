using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class Product
{
    public int ProductId { get; set; }
    public int ProductNumber { get; set; }
    [MinLength(5)]
    public string ProductName { get; set; }
    public int PricePrKilo { get; set; }
    public string ProductType { get; set; }
    public string CountryOfBirth { get; set; }
    public string ProductionCountry { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public DateTime MinExpDate { get; set; }

}