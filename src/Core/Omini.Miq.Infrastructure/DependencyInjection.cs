using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Omini.Miq.Domain.Repositories;
using Omini.Miq.Domain.Transactions;
using Omini.Miq.Infrastructure.Interceptors;
using Omini.Miq.Infrastructure.Repositories;

namespace Omini.Miq.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditableInterceptor>();
        services.AddSingleton<SoftDeletableInterceptor>();

        services.AddDbContext<MiqContext>((sp, opt) =>
        {
            opt.AddInterceptors(
                sp.GetRequiredService<AuditableInterceptor>(),
                sp.GetRequiredService<SoftDeletableInterceptor>()
            );
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        //services.AddScoped<IOpmeUserRepository, OpmeUserRepository>();

        services.AddScoped<IPromissoryRepository, PromissoryRepository>();

        services.AddScoped<IUnitOfWork, IUnitOfWork>();

        // services.AddHealthChecks()
        //         .AddDbContextCheck<OpmeContext>("Database");

        return services;
    }
}