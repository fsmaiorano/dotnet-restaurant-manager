using IntegrationTest.Product.Commands;
using Microsoft.VisualBasic;

namespace IntegrationTest;

public class DeleteProductCommandTest : Testing
{
    private int _createdProductId;

    [TestInitialize]
    public void TestInitialize()
    {
        var productEntity = CreateProductCommandTest.GenerateProductEntity();
        AddAsync(productEntity).GetAwaiter().GetResult();
        _createdProductId = productEntity.Id;
    }

    [TestMethod]
    public async Task ShouldDeleteProductUseCase()
    {

    }
}
