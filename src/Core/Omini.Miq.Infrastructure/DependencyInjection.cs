using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omini.Miq.Domain;
using Omini.Miq.Domain.Externals.Repositories;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Infrastructure.Externals.Repositories;
using Omini.Miq.Infrastructure.Interceptors;
using Omini.Miq.Infrastructure.Repositories;
using Omini.Miq.Infrastructure.Transaction;

namespace Omini.Miq.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditableInterceptor>();
        services.AddSingleton<SoftDeletableInterceptor>();

        services.AddSingleton<IAuthenticationService, AuthenticationService>();

        services.AddDbContext<MiqContext>((sp, opt) =>
        {
            opt.AddInterceptors(
                sp.GetRequiredService<AuditableInterceptor>(),
                sp.GetRequiredService<SoftDeletableInterceptor>()
            );
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),   
                x => x.MigrationsAssembly("Omini.Miq.Migrations"));
        });

        //services.AddScoped<IOpmeUserRepository, OpmeUserRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        services.AddScoped<IPromissoryRepository, PromissoryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });

        services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
                {
                    jwtOptions.Authority = configuration["Authentication:ValidIssuer"];
                    jwtOptions.Audience = configuration["Authentication:Audience"];
                    jwtOptions.TokenValidationParameters.ValidIssuer = configuration["Authentication:ValidIssuer"];
                });

        // services.AddHealthChecks()
        //         .AddDbContextCheck<OpmeContext>("Database");

        return services;
    }
}