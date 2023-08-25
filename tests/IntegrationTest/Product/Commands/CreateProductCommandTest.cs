using Application.UseCases.Product.Commands.CreateProduct;
using Bogus;
using Domain.Entities;
using IntegrationTest.Restaurant.Commands;

namespace IntegrationTest.Product.Commands;

[TestClass]
public class CreateProductCommandTest : Testing
{
    private int _createdRestaurantId;

    [TestInitialize]
    public async void TestInitialize()
    {
        var restaurantEntity = CreateRestaurantTest.GenerateRestaurantEntity();
        await AddAsync(restaurantEntity);

        _createdRestaurantId = restaurantEntity.Id;
    }

    [TestMethod]
    public async Task ShouldCreateProductUseCase()
    {
        var createProductCommand = GenerateCreateProductCommand();

        createProductCommand.RestaurantId = _createdRestaurantId;

        var createdProductId = await SendAsync(createProductCommand);
        Assert.IsNotNull(createdProductId);
        Assert.IsTrue(createdProductId > 0);
    }

    [DataTestMethod]
    public static CreateProductCommand GenerateCreateProductCommand()
    {
        return new Faker<CreateProductCommand>()
                     .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                     .RuleFor(x => x.Price, f => f.Random.Decimal(0, 100))
                     .RuleFor(x => x.ImageUrl, f => f.Image.PicsumUrl())
                     .RuleFor(x => x.Promotions, f => new List<PromotionEntity>())
                     .Generate();
    }
}
