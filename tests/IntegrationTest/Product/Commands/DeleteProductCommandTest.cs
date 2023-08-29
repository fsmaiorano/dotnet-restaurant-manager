using Application.UseCases.Product.Commands.DeleteProduct;
using Domain.Entities;
using IntegrationTest.Restaurant.Commands;

namespace IntegrationTest.Product.Commands;

[TestClass]
public class DeleteProductCommandTest : Testing
{
    private int _createdProductId;
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
        _createdProductId = productEntity.Id;
    }

    [TestMethod]
    public async Task ShouldDeleteProductUseCase()
    {
        await SendAsync(new DeleteProductCommand(_createdProductId));

        var product = await FindAsync<ProductEntity>(_createdProductId);
        Assert.IsNull(product);
    }

    [TestMethod]
    public async Task ShouldDeleteProductController()
    {
        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/Product?id={_createdProductId}");

        var product = await FindAsync<ProductEntity>(_createdProductId);
        Assert.IsNull(product);
    }
}
