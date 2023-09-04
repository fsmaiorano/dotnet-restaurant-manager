
using Application.UseCases.Promotion.Commands.UpdatePromotionCommand;
using Domain.Entities;

namespace IntegrationTest.Promotion.Commands;

[TestClass]
public class UpdatePromotionCommandTest : Testing
{
    private int _createdPromotionId;

    [TestInitialize]
    public void TestInitialize()
    {
        var promotionEntity = CreatePromotionCommandTest.GeneratePromotionEntity();
        AddAsync(promotionEntity).GetAwaiter().GetResult();
        _createdPromotionId = promotionEntity.Id;

    }

    [TestMethod]
    public async Task ShouldUpdatePromotionUseCaseAsync()
    {
        var promotion = await FindAsync<PromotionEntity>(_createdPromotionId);

        promotion!.Description = $"updated_{promotion.Description}";

        await SendAsync(new UpdatePromotionCommand
        {
            Id = promotion.Id,
            ProductId = promotion.ProductId,
            Description = promotion.Description,
            PromotionalPrice = promotion.PromotionalPrice,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate,
        });

        var updatedPromotion = await FindAsync<PromotionEntity>(_createdPromotionId);
        Assert.IsNotNull(updatedPromotion);
        Assert.IsTrue(promotion.Description == updatedPromotion.Description);
    }

    [TestMethod]
    public async Task ShouldNotUpdatePromotionUseCaseAsync()
    {
        var promotion = await FindAsync<PromotionEntity>(_createdPromotionId);

        promotion!.Description = $"updated_{promotion.Description}";

        await SendAsync(new UpdatePromotionCommand
        {
            Id = promotion.Id,
            ProductId = promotion.ProductId,
            Description = promotion.Description,
            PromotionalPrice = promotion.PromotionalPrice,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate,
        });

        var updatedPromotion = await FindAsync<PromotionEntity>(_createdPromotionId);
        Assert.IsNotNull(updatedPromotion);
        Assert.IsTrue(promotion.Description == updatedPromotion.Description);
    }
}

