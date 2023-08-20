using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace Infrastructure.Context;

public class DataContext : DbContext, IDataContext
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DataContext() : base()
    {

    }

    public DataContext(
       DbContextOptions<DataContext> options,
       IMediator mediator,
       IConfiguration configuration,
       AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _configuration = configuration;
    }

    // public DbSet<TagEntity> Tags => Set<TagEntity>();
    // public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    // public DbSet<PostEntity> Posts => Set<PostEntity>();
    // public DbSet<RestaurantEntity> Restaurants => Set<RestaurantEntity>();

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<PromotionEntity> Promotions => Set<PromotionEntity>();
    public DbSet<RestaurantEntity> Restaurants => Set<RestaurantEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    /// <summary>
    /// Only used to create migrations and update local database
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);

        if (AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
            optionsBuilder.UseInMemoryDatabase("ApplicationDb");
        }
        else
        {
            if (!string.IsNullOrEmpty(_configuration.GetConnectionString("DefaultConnection")))
            {
                Console.WriteLine($"Using SQL Server - ConnectionString: {_configuration.GetConnectionString("DefaultConnection")}");
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            }
            else
            {
                Console.WriteLine($"Using SQL Server - ConnectionString: TestDb");

                var solutionPath = GetSolutionPath().FullName ?? string.Empty;
                var path = Path.Combine(solutionPath, "tests", "IntegrationTest", "bin", "Debug", "net7.0", "appsettings.json");

                Console.WriteLine($"Using SQL Server TestDb - Path: {path}");

                var testAppSettings = new ConfigurationBuilder()
                         .AddJsonFile(path)
                         .Build();

                var connectionString = testAppSettings.GetConnectionString("DefaultConnection");
                Console.WriteLine($"Using SQL Server TestDb - ConnectionString: {connectionString}");

                optionsBuilder
                    .UseSqlite(connectionString, builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            }
        }

        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        _mediator.DispatchDomainEvents(this).Wait();

        return base.SaveChanges();
    }

    private static DirectoryInfo GetSolutionPath(string currentPath = null!)
    {
        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory!;
    }
}
