using System.Text;
using Application.UseCases.Restaurant.Commands.UpdateRestaurant;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Restaurant.Commands;

[TestClass]
public class UpdateRestaurantCommandTest : Testing
{
    [TestMethod]
    public async Task ShouldUpdateRestaurantUseCase()
    {
        var createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();
        var createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);

        var Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

        Assert.IsNotNull(Restaurant);
        Assert.IsTrue(Restaurant.Id > 0);
        Assert.IsTrue(Restaurant.Name == createRestaurantCommand.Name);

        Restaurant.Name = $"updated_{Restaurant.Name}";

        await SendAsync(new UpdateRestaurantCommand
        {
            Id = Restaurant.Id,
            Name = Restaurant.Name,
        });

        Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

        Assert.IsNotNull(Restaurant);
        Assert.IsTrue(Restaurant.Name == $"updated_{createRestaurantCommand.Name}");
    }

    // [TestMethod]
    // public async Task ShouldUpdateRestaurantController()
    // {
    //     var createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();
    //     var createdRestaurantId = await SendAsync(createRestaurantCommand);
    //     Assert.IsNotNull(createdRestaurantId);
    //     Assert.IsTrue(createdRestaurantId > 0);

    //     var Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

    //     Assert.IsNotNull(Restaurant);
    //     Assert.IsTrue(Restaurant.Id > 0);
    //     Assert.IsTrue(Restaurant.Name == createRestaurantCommand.Name);

    //     Restaurant.Name = $"updated_{Restaurant.Name}";

    //     var updateRestaurantCommand = new UpdateRestaurantCommand
    //     {
    //         Id = Restaurant.Id,
    //         Name = Restaurant.Name,
    //     };

    //     using var client = await CreateHttpClient();
    //     var response = await client.PutAsync($"/api/Restaurant?id={Restaurant.Id}", new StringContent(JsonConvert.SerializeObject(updateRestaurantCommand), Encoding.UTF8, "application/json"));
    //     Assert.IsTrue(response.IsSuccessStatusCode);

    //     Restaurant = await FindAsync<RestaurantEntity>(createdRestaurantId);

    //     Assert.IsNotNull(Restaurant);
    //     Assert.IsTrue(Restaurant.Name == $"updated_{createRestaurantCommand.Name}");
    // }
}
