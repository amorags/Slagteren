using Dapper;
using infrastructure.DataModels;
using infrastructure.QueryModels;
using Npgsql;

namespace infrastructure.Repositories;

public class ProductRepository
{
    private NpgsqlDataSource _dataSource;

    public ProductRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<ProductFeedQuery> GetProductFeed()
    {
        string sql = $@"
SELECT productId as {nameof(ProductFeedQuery.ProductId)},
        productNumber as {nameof(ProductFeedQuery.ProductNumber)},
        productName as {nameof(ProductFeedQuery.ProductNumber)},
        pricePrKilo as {nameof(ProductFeedQuery.PricePrKilo)},
        productType as {nameof(ProductFeedQuery.ProductType)},
        countryOfBirth as {nameof(ProductFeedQuery.CountryOfBirth)},
        productionCountry as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        imgUrl as {nameof(ProductFeedQuery.ImgUrl)},
        minEinDate{nameof(ProductFeedQuery.MinExpDate)}
        FROM DinSlagter.products
    ";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<ProductFeedQuery>(sql);
        }
    }

    public Product CreateProduct(int productNumber, string productName, int pricePrKilo, string productType,
        string countryOfBirth, string productionCountry, string description, string imgUrl, DateTime minExpDate)
    {
        string sql = $@"
INSERT INTO DinSlagter.products (productNumber, productName, pricePrKilo, productType, countryOfBirth, productionCountry, description, imgUrl, minExpDate)
VALUES (@productNumber, @productName, @pricePrKilo, @productType, @countryOfBirth, @productionCountry, @description, @imgUrl, @minExpDate)
RETURNING productId as {nameof(ProductFeedQuery.ProductId)},
        productNumber as {nameof(ProductFeedQuery.ProductNumber)},
        productName as {nameof(ProductFeedQuery.ProductNumber)},
        pricePrKilo as {nameof(ProductFeedQuery.PricePrKilo)},
        productType as {nameof(ProductFeedQuery.ProductType)},
        countryOfBirth as {nameof(ProductFeedQuery.CountryOfBirth)},
        productionCountry as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        imgUrl as {nameof(ProductFeedQuery.ImgUrl)},
        minEinDate{nameof(ProductFeedQuery.MinExpDate)};
";
        
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Product>(sql, new { productNumber, productName, pricePrKilo, productType, countryOfBirth,
                productionCountry, description, imgUrl, minExpDate});
        }
    }

    public Product UpdateProduct(int productId, int productNumber, string productName, int pricePrKilo, string productType,
        string countryOfBirth, string productionCountry, string description, string imgUrl, DateTime minExpDate)
    {
        string sql = $@"
UPDATE DinSlagter.products SET productName = @productName, pricePrKilo = @pricePrKilo, countryOfBirth = @countryOfBirth,
        productionCountry = @productionCountry, description = @description, imgUrl = @imgUrl, minExpDate = @minExpDate
WHERE productId = @productId
RETURNING productId as {nameof(ProductFeedQuery.ProductId)},
        productNumber as {nameof(ProductFeedQuery.ProductNumber)},
        productName as {nameof(ProductFeedQuery.ProductNumber)},
        pricePrKilo as {nameof(ProductFeedQuery.PricePrKilo)},
        productType as {nameof(ProductFeedQuery.ProductType)},
        countryOfBirth as {nameof(ProductFeedQuery.CountryOfBirth)},
        productionCountry as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        imgUrl as {nameof(ProductFeedQuery.ImgUrl)},
        minEinDate{nameof(ProductFeedQuery.MinExpDate)};
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Product>(sql, new
            {
                productId, productNumber, productName, pricePrKilo, productType, countryOfBirth,
                productionCountry, description, imgUrl, minExpDate
            });
        }
    }

    public bool DeleteProduct(int productId)
    {
        var sql = @"DELETE FROM DinSlagter.products WHERE productId = @productId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { productId }) == 1;
        }
    }
    public bool DoesProductWithNameExist(string productName)
    {
        var sql = $@"SELECT COUNT(*) FROM DinSlagter.products WHERE productName = @productName;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { productName}) == 1;
        }
    }
}