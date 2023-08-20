﻿using Application.Common.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using ZymLabs.NSwag.FluentValidation;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        //services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<DataContext>();

        services.AddControllersWithViews();

        services.AddRazorPages();

        services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        //services.AddOpenApiDocument((configure, serviceProvider) =>
        //{
        //    var fluentValidationSchemaProcessor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

        //    // Add the fluent validations schema processor
        //    configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);

        //    configure.Title = "Application API";
        //    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        //    {
        //        Type = OpenApiSecuritySchemeType.ApiKey,
        //        Name = "Authorization",
        //        In = OpenApiSecurityApiKeyLocation.Header,
        //        Description = "Type into the textbox: Bearer {your JWT token}."
        //    });

        //    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        //});

        return services;
    }
}
