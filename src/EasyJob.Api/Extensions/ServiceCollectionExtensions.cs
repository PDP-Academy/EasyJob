using EasyJob.Application.Services.Authentication;
using EasyJob.Application.Services.Users;
using EasyJob.Application.Validator;
using EasyJob.Infrastructure.Authentication;
using EasyJob.Infrastructure.Contexts;
using EasyJob.Infrastructure.Repositories.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EasyJob.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.Configure<JwtOption>(configuration
                .GetSection("JwtSettings"));

            services.AddSwaggerService();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("EasyJob");
                /*options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });*/
            });

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IUserFactory, UserFactory>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddValidatorsFromAssemblyContaining<UserForCreationDtoValidator>();

            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }

        public static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        private static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AloShop.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
        }
    }
}
