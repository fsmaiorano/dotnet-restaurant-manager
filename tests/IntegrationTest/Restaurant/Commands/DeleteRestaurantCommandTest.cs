using Application.UseCases.Restaurant.Commands.DeleteRestaurant;
using Domain.Entities;

namespace IntegrationTest.Restaurant.Commands;

[TestClass]
public class DeleteRestaurantCommandTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldDeleteRestaurantUseCase()
    {
        var createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();
        var createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);

        await SendAsync(new DeleteRestaurantCommand(createdRestaurantId));

        var Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);
        Assert.IsNull(Restaurant);
    }

    // [TestMethod]
    // public async Task ShouldDeleteRestaurantController()
    // {
    //     var createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();
    //     var createdRestaurantId = await SendAsync(createRestaurantCommand);
    //     Assert.IsNotNull(createdRestaurantId);
    //     Assert.IsTrue(createdRestaurantId > 0);

    //     using var client = await CreateHttpClient();
    //     var response = await client.DeleteAsync($"/api/Restaurant?id={createdRestaurantId}");

    //     var Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);
    //     Assert.IsNull(Restaurant);
    // }
}
