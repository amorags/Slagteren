namespace infrastructure.DataModels;

public class Products
{
    public int Product_Id { get; set; }
    public int ProductNumber { get; set; }
    public int PricePrKilo { get; set; }
    public string ProductType { get; set; }
    public string CountryOfBirth { get; set; }
    public string ProductionCountry { get; set; }
    public string Description { get; set; }
    public string ImgUrl { get; set; }
    public DateTime MinExpDate { get; set; }

}