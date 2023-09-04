using System.Text;
using Application.UseCases.Promotion.Commands.CreatePromotion;
using Bogus;
using Domain.Entities;
using IntegrationTest.Product.Commands;
using IntegrationTest.Restaurant.Commands;
using Newtonsoft.Json;

namespace IntegrationTest.Promotion.Commands;

[TestClass]
public class CreatePromotionCommandTest : Testing
{
    private int _createdProductId;
    [TestInitialize]
    public void TestInitialize()
    {
        var restaurantEntity = CreateRestaurantCommandTest.GenerateRestaurantEntity();
        AddAsync(restaurantEntity).GetAwaiter().GetResult();
        var createdRestaurantId = restaurantEntity.Id;

        var productEntity = CreateProductCommandTest.GenerateProductEntity();
        productEntity.RestaurantId = createdRestaurantId;
        AddAsync(productEntity).GetAwaiter().GetResult();
        _createdProductId = productEntity.Id;
    }

    [TestMethod]
    public async Task ShouldCreatePromotion()
    {
        var command = GenerateCreatePromotionCommand();
        command.ProductId = _createdProductId;

        var createdProductId = await SendAsync(command);
        Assert.IsNotNull(createdProductId);
        Assert.IsTrue(createdProductId > 0);
    }

    [TestMethod]
    public async Task ShouldCreatePromotionController()
    {
        var command = GenerateCreatePromotionCommand();
        command.ProductId = _createdProductId;

        using var client = await CreateHttpClient();
        var response = await client.PostAsync("/api/Promotion", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [DataTestMethod]
    public static CreatePromotionCommand GenerateCreatePromotionCommand()
    {
        return new Faker<CreatePromotionCommand>()
                        .RuleFor(x => x.Description, f => f.Commerce.Categories(1)[0])
                        .RuleFor(x => x.PromotionalPrice, f => f.Random.Decimal(0, 100))
                        .RuleFor(x => x.StartDate, f => f.Date.Past())
                        .RuleFor(x => x.EndDate, f => f.Date.Future())
                     .Generate();
    }

    [DataTestMethod]
    public static PromotionEntity GeneratePromotionEntity()
    {
        return new Faker<PromotionEntity>()
                     .CustomInstantiator(f => new PromotionEntity(productId: 1,
                                             description: f.Commerce.Categories(1)[0],
                                             promotionalPrice: f.Random.Decimal(0, 100),
                                             startDate: DateTime.Now,
                                             endDate: DateTime.Now.AddDays(30)))
                     .RuleFor(x => x.Description, f => f.Commerce.Categories(1)[0])
                     .RuleFor(x => x.PromotionalPrice, f => f.Random.Decimal(0, 100))
                     .Generate();
    }
}
