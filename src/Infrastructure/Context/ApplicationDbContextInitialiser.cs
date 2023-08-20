using Application.UseCases.User.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Context;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly DataContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly IMediator _mediator;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, DataContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context;
        _mediator = mediator;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.EnsureCreatedAsync(
                    cancellationToken: CancellationToken.None);

                try
                {
                    await _context.Database.MigrateAsync();
                }
                catch (Exception) { }
            }

            if (_context.Database.IsInMemory())
                await _context.Database.EnsureCreatedAsync(
                    cancellationToken: CancellationToken.None);

            if (_context.Database.IsSqlite())
            {
                await _context.Database.EnsureDeletedAsync(
                    cancellationToken: CancellationToken.None);

                await _context.Database.EnsureCreatedAsync(
                    cancellationToken: CancellationToken.None);

                try
                {
                    await _context.Database.MigrateAsync();
                }
                catch (Exception) { }
            }

            await SeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Users.Any())
        {
            var createUserCommand = new CreateUserCommand()
            {
                Name = "Admin",
                Email = "admin@application.com",
                PasswordHash = "12345678"
            };

            await _mediator.Send(createUserCommand);
        }

        return;
    }
}
