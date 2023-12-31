﻿using Application.Common.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Context;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(provider => configuration);

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

        if (!AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
            services.AddDbContext<DataContext>(options =>
            {
                // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
        }
        else {
             services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase("ApplicationDb");
            });
        }

        return services;
    }
}
