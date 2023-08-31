using System.Text;
using Application.UseCases.Restaurant.Commands.UpdateRestaurant;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Restaurant.Commands;

[TestClass]
public class UpdateRestaurantCommandTest : Testing
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
    public async Task ShouldUpdateRestaurantUseCase()
    {
        var restaurant = await FindAsync<RestaurantEntity>(_createdRestaurantId);

        restaurant!.Name = $"updated_{restaurant.Name}";

        await SendAsync(new UpdateRestaurantCommand
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            ImageUrl = restaurant.ImageUrl ?? string.Empty,
        });

        var updatedRestaurant = await FindAsync<RestaurantEntity>(_createdRestaurantId);
        Assert.IsNotNull(updatedRestaurant);
        Assert.IsTrue(restaurant.Name == updatedRestaurant.Name);
    }

    [TestMethod]
    public async Task ShouldUpdateRestaurantController()
    {
        var createRestaurantCommand = CreateRestaurantCommandTest.GenerateCreateRestaurantCommand();
        var createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);

        var Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

        Assert.IsNotNull(Restaurant);
        Assert.IsTrue(Restaurant.Id > 0);
        Assert.IsTrue(Restaurant.Name == createRestaurantCommand.Name);

        Restaurant.Name = $"updated_{Restaurant.Name}";

        var updateRestaurantCommand = new UpdateRestaurantCommand
        {
            Id = Restaurant.Id,
            Name = Restaurant.Name,
            Address = Restaurant.Address,
            ImageUrl = Restaurant.ImageUrl ?? string.Empty,
        };

        using var client = await CreateHttpClient();
        var response = await client.PutAsync($"/api/Restaurant?id={Restaurant.Id}", new StringContent(JsonConvert.SerializeObject(updateRestaurantCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);

        Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

        Assert.IsNotNull(Restaurant);
        Assert.IsTrue(Restaurant.Name == $"updated_{createRestaurantCommand.Name}");
    }
}
