using System.Text;
using Application.UseCases.Product.Commands.UpdateProduct;
using Domain.Entities;
using IntegrationTest.Restaurant.Commands;
using Newtonsoft.Json;

namespace IntegrationTest.Product.Commands;

[TestClass]
public class UpdateProductCommandTest : Testing
{
    private int _createdRestaurantId;
    private int _createdProductId;

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
    public async Task ShouldUpdateProductUseCase()
    {
        var product = await FindAsync<ProductEntity>(_createdProductId);

        product!.Name = $"updated_{product.Name}";

        var updateProductCommand = new UpdateProductCommand(id: product.Id,
                                                            name: product.Name,
                                                            price: product.Price,
                                                            restaurantId: product.RestaurantId)
        {
            ImageUrl = product.ImageUrl,
            Promotions = product.Promotions,
        };

        await SendAsync(updateProductCommand);

        var updatedProduct = await FindAsync<ProductEntity>(_createdProductId);

        Assert.IsNotNull(updatedProduct);
        Assert.IsTrue(updatedProduct!.Name == product.Name);
    }

    [TestMethod]
    public async Task ShouldUpdateProductController()
    {
        var product = await FindAsync<ProductEntity>(_createdProductId);

        product!.Name = $"updated_{product.Name}";

        var updateProductCommand = new UpdateProductCommand(id: product.Id,
                                                            name: product.Name,
                                                            price: product.Price,
                                                            restaurantId: product.RestaurantId)
        {
            ImageUrl = product.ImageUrl,
            Promotions = product.Promotions,
        };
        var response = await PutAsync($"/api/Product?id={updateProductCommand.Id}", updateProductCommand);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var updatedProduct = await FindAsync<ProductEntity>(_createdProductId);

        Assert.IsNotNull(updatedProduct);
        Assert.IsTrue(updatedProduct!.Name == $"{product.Name}");
    }
}
