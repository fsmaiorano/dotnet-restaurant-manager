using System.Text;
using Application.UseCases.Restaurant.Commands.CreateRestaurant;
using Bogus;
using Domain.Entities;
using Newtonsoft.Json;

namespace IntegrationTest.Restaurant.Commands;

[TestClass]
public class CreateRestaurantCommandTest : Testing
{
    [TestInitialize]
    public void TestInitialize()
    {

    }

    [TestMethod]
    public async Task ShouldCreateRestaurantUseCase()
    {
        var createRestaurantCommand = GenerateCreateRestaurantCommand();

        var createdRestaurantId = await SendAsync(createRestaurantCommand);
        Assert.IsNotNull(createdRestaurantId);
        Assert.IsTrue(createdRestaurantId > 0);
    }

    [TestMethod]
    public async Task ShouldCreateRestaurantController()
    {
        var createRestaurantCommand = GenerateCreateRestaurantCommand();

        using var client = await CreateHttpClient();
        var response = await client.PostAsync("/api/Restaurant", new StringContent(JsonConvert.SerializeObject(createRestaurantCommand), Encoding.UTF8, "application/json"));
        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [DataTestMethod]
    public static CreateRestaurantCommand GenerateCreateRestaurantCommand()
    {
        return new Faker<CreateRestaurantCommand>()
                     .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0])
                     .RuleFor(x => x.Address, f => f.Address.FullAddress())
                     .Generate();
    }

    [DataTestMethod]
    public static RestaurantEntity GenerateRestaurantEntity()
    {
        return new Faker<RestaurantEntity>()
                     .CustomInstantiator(f => new RestaurantEntity(name: f.Commerce.Categories(1)[0], address: f.Address.FullAddress()))
                     .RuleFor(x => x.Id, f => f.Random.Int(1, 100))
                     .RuleFor(x => x.Address, f => f.Address.FullAddress())
                     .Generate();
    }
}
