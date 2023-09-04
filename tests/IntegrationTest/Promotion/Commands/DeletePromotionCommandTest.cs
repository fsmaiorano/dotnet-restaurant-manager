using IntegrationTest.Product.Commands;
using IntegrationTest.Restaurant.Commands;
using IntegrationTest.Promotion.Commands;
using Application.UseCases.Promotion.Commands.DeletePromotionCommand;
using Domain.Entities;

namespace IntegrationTest.Promotion.Commands;

[TestClass]
public class DeletePromotionCommandTest : Testing
{
    private int _createdProductId;
    private int _createdPromotionId;

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

        var promotionEntity = CreatePromotionCommandTest.GeneratePromotionEntity();
        promotionEntity.ProductId = _createdProductId;
        AddAsync(promotionEntity).GetAwaiter().GetResult();
        _createdPromotionId = promotionEntity.Id;
    }

    [TestMethod]
    public async Task ShouldDeletePromotion()
    {
        await SendAsync(new DeletePromotionCommand(_createdPromotionId));

        var promotion = await FindAsync<PromotionEntity>(_createdPromotionId);
        Assert.IsNull(promotion);
    }
}
