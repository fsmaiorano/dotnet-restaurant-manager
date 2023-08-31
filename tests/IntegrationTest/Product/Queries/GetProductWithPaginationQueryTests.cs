
using Application.UseCases.Product.Queries.GetProduct;
using Application.UseCases.Product.Queries.GetProductWithPaginationQuery;
using IntegrationTest.Product.Commands;

namespace IntegrationTest.Product.Queries;

[TestClass]
public class GetProductWithPaginationQueryTests : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var createProductCommand = CreateProductCommandTest.GenerateCreateProductCommand();

        var createdProductId = await SendAsync(createProductCommand);
        Assert.IsNotNull(createdProductId);
        Assert.IsTrue(createdProductId > 0);

        createProductCommand = CreateProductCommandTest.GenerateCreateProductCommand();

        createdProductId = await SendAsync(createProductCommand);
        Assert.IsNotNull(createdProductId);
        Assert.IsTrue(createdProductId > 0);
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
