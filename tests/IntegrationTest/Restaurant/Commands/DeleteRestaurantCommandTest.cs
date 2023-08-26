using Application.UseCases.Restaurant.Commands.DeleteRestaurant;
using Domain.Entities;

namespace IntegrationTest.Restaurant.Commands;

[TestClass]
public class DeleteRestaurantCommandTest : Testing
{
    private int _createdRestaurantId;

    [TestInitialize]
    public void TestInitialize()
    {
        var restaurantEntity = CreateRestaurantCommandTest.GenerateRestaurantEntity();
        AddAsync(restaurantEntity).GetAwaiter().GetResult();
        _createdRestaurantId = restaurantEntity.Id;
    }

    [TestMethod]
    public async Task ShouldDeleteRestaurantUseCase()
    {
        await SendAsync(new DeleteRestaurantCommand(_createdRestaurantId));

        var restaurant = await FindAsync<RestaurantEntity>(_createdRestaurantId);
        Assert.IsNull(restaurant);
    }

    [TestMethod]
    public async Task ShouldDeleteRestaurantController()
    {
        using var client = await CreateHttpClient();
        var response = await client.DeleteAsync($"/api/Restaurant?id={_createdRestaurantId}");

        var restaurant = await FindAsync<RestaurantEntity>(_createdRestaurantId);
        Assert.IsNull(restaurant);
    }
}
