using Application.Common.Models;
using Application.UseCases.Restaurant.Queries.GetRestaurant;
using Bogus;
using Domain.Entities;
using IntegrationTest.Restaurant.Commands;
using Newtonsoft.Json;

namespace IntegrationTest.Restaurant.Queries;

[TestClass]
public class GetRestaurantWithPaginationQueryTests : Testing
{
    [TestInitialize]
    public async Task TestInitialize()
    {
        var createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();

        var createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);

        createRestaurantCommand = CreateRestaurantTest.GenerateCreateRestaurantCommand();

        createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);
    }

    [TestMethod]
    public async Task ShouldReturnAllRestaurant()
    {
        var query = new GetRestaurantQuery();
        var result = await SendAsync(query);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }


    [TestMethod]
    public async Task ShouldReturnPaginatedListWithRestaurantUseCase()
    {
        var query = new GetRestaurantWithPaginationQuery() { PageSize = 9999, PageNumber = 1 };
        var result = await SendAsync(query);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }

    [TestMethod]
    public async Task ShouldReturnPaginatedListWithRestaurantController()
    {
        using var client = await CreateHttpClient();
        var response = await client.GetAsync("/api/Restaurant?PageSize=9999&PageNumber=1");
        Assert.IsTrue(response.IsSuccessStatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.IsNotNull(content);

        var result = JsonConvert.DeserializeObject<PaginatedList<RestaurantEntity>>(content);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Items.Count > 0);
    }
}
