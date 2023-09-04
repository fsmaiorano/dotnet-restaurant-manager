using Application.UseCases.Promotion.Queries.GetPromotion;
using IntegrationTest.Product.Commands;
using IntegrationTest.Promotion.Commands;

namespace IntegrationTest.Promotion.Queries;

[TestClass]
public class GetPromotionWithPaginationQueryTest : Testing
{
    private int _createdProductId;

    [TestInitialize]
    public void TestInitialize()
    {
        var productEntity = CreateProductCommandTest.GenerateProductEntity();
        AddAsync(productEntity).GetAwaiter().GetResult();
        _createdProductId = productEntity.Id;

        var promotionEntity = CreatePromotionCommandTest.GeneratePromotionEntity();
        promotionEntity.ProductId = _createdProductId;
        AddAsync(promotionEntity).GetAwaiter().GetResult();
    }

    [TestMethod]
    public async Task ShouldReturnAllPromotion()
    {
        var query = new GetPromotionQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }
    
}
