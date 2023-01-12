﻿using EasyJob.Application.Services.Users;
using EasyJob.Application.Validator;
using EasyJob.Infrastructure.Contexts;
using EasyJob.Infrastructure.Repositories.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IUserFactory, UserFactory>();

            services.AddValidatorsFromAssemblyContaining<UserForCreationDtoValidator>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
