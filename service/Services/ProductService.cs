using System.ComponentModel.DataAnnotations;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using infrastructure.Repositories;

namespace service.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public IEnumerable<ProductFeedQuery> GetProductFeed()
    {
        return _productRepository.GetProductFeed();
    }

    public Product CreateProduct(int productNumber, string productName, int pricePrKilo, string productType, 
        string countryOfBirth, string productionCountry, string description, string imgUrl, DateTime minExpDate)
    {
        var doesProductExist = _productRepository.DoesProductWithNameExist(productName);
        if (!doesProductExist)
        {
            throw new ValidationException("A product already exists with this name " + productName);
        }

        return _productRepository.CreateProduct(productNumber, productName, pricePrKilo, productType, countryOfBirth,
            productionCountry, description, imgUrl, minExpDate);
    }

    public Product UpdateProduct(int productId, int productNumber, string productName, int pricePrKilo, 
        string productType, string countryOfBirth, string productionCountry, string description, string imgUrl, DateTime minExpDate)
    {
        return _productRepository.UpdateProduct(productId, productNumber, productName, pricePrKilo, productType, countryOfBirth,
            productionCountry, description, imgUrl, minExpDate);
    }
    
    public void DeleteProduct(int productId)
    {
        var result = _productRepository.DeleteProduct(productId);
        if (!result)
        {
            throw new Exception("Could not Delete Product");
        }
    }

    
}