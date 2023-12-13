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
SELECT product_id as {nameof(ProductFeedQuery.ProductId)},
        product_number as {nameof(ProductFeedQuery.ProductNumber)},
        product_name as {nameof(ProductFeedQuery.ProductName)},
        price_pr_kilo as {nameof(ProductFeedQuery.PricePrKilo)},
        type_id as {nameof(ProductFeedQuery.ProductType)},
        country_of_birth as {nameof(ProductFeedQuery.CountryOfBirth)},
        production_country as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        img_url as {nameof(ProductFeedQuery.ImgUrl)},
        min_exp_date as {nameof(ProductFeedQuery.MinExpDate)}
        FROM dinslagter.products
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
INSERT INTO dinslagter.products (product_number, product_name, price_pr_kilo, type_id, country_of_birth, production_country, description, img_url, min_exp_date)
VALUES (@productNumber, @productName, @pricePrKilo, @productType, @countryOfBirth, @productionCountry, @description, @imgUrl, @minExpDate)
RETURNING product_id as {nameof(ProductFeedQuery.ProductId)},
        product_number as {nameof(ProductFeedQuery.ProductNumber)},
        product_name as {nameof(ProductFeedQuery.ProductName)},
        price_pr_kilo as {nameof(ProductFeedQuery.PricePrKilo)},
        type_id as {nameof(ProductFeedQuery.ProductType)},
        country_of_birth as {nameof(ProductFeedQuery.CountryOfBirth)},
        production_country as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        img_url as {nameof(ProductFeedQuery.ImgUrl)},
        min_exp_date as {nameof(ProductFeedQuery.MinExpDate)};
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
UPDATE dinslagter.products SET product_name = @productName, price_pr_kilo = @pricePrKilo, country_of_birth = @countryOfBirth,
        production_country = @productionCountry, description = @description, img_url = @imgUrl, min_exp_date = @minExpDate
WHERE product_id = @productId
RETURNING product_id as {nameof(ProductFeedQuery.ProductId)},
        product_number as {nameof(ProductFeedQuery.ProductNumber)},
        product_name as {nameof(ProductFeedQuery.ProductName)},
        price_pr_kilo as {nameof(ProductFeedQuery.PricePrKilo)},
        type_id as {nameof(ProductFeedQuery.ProductType)},
        country_of_birth as {nameof(ProductFeedQuery.CountryOfBirth)},
        production_country as {nameof(ProductFeedQuery.ProductionCountry)},
        description as {nameof(ProductFeedQuery.Description)},
        img_url as {nameof(ProductFeedQuery.ImgUrl)},
        min_exp_date as {nameof(ProductFeedQuery.MinExpDate)};
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
        var sql = @"DELETE FROM dinslagter.products WHERE product_id = @productId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new { productId }) == 1;
        }
    }
    public bool DoesProductWithNameExist(string productName)
    {
        var sql = $@"SELECT COUNT(*) FROM dinslagter.products WHERE product_name = @productName;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql, new { productName}) == 1;
        }
    }
}