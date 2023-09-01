
using Application.UseCases.Product.Queries.GetProduct;
using Application.UseCases.Product.Queries.GetProductWithPaginationQuery;
using IntegrationTest.Product.Commands;
using IntegrationTest.Restaurant.Commands;

namespace IntegrationTest.Product.Queries;

[TestClass]
public class GetProductWithPaginationQueryTests : Testing
{
    private int _createdRestaurantId;

    [TestInitialize]
    public void TestInitialize()
    {
        var restaurantEntity = CreateRestaurantCommandTest.GenerateRestaurantEntity();
        AddAsync(restaurantEntity).GetAwaiter().GetResult();
        _createdRestaurantId = restaurantEntity.Id;

        var productEntity = CreateProductCommandTest.GenerateProductEntity();
        productEntity.RestaurantId = _createdRestaurantId;
        AddAsync(productEntity).GetAwaiter().GetResult();
    }

    [TestMethod]
    public async Task ShouldReturnAllProduct()
    {
        var query = new GetProductQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }

    [TestMethod]
    public async Task ShouldReturnPaginatedListWithProductUseCase()
    {
        var query = new GetProductWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }

}
