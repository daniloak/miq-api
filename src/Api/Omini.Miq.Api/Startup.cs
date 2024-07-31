using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using FluentValidation.AspNetCore;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Omini.Miq.Shared.Services.Security;
using Omini.Miq.Api.Services.Security;
using Omini.Miq.Api.Configuration;
using Omini.Miq.Infrastructure;
using Omini.Miq.Business;
using Omini.Miq.Middlewares;
using Omini.Miq.Api.Middlewares;

internal class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(WebApplicationBuilder app, IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IClaimsService, ClaimsService>();

        services.AddEndpointsApiExplorer();
        //services.AddSwaggerConfiguration(Configuration);

        services.AddAuthenticationConfiguration(Configuration);

        services.AddInfrastructure(Configuration);

        services.AddBusiness();

        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });

        services.AddAutoMapper(typeof(Startup));
        services.AddVersionConfiguration();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthUsePkce();
                c.OAuthClientId(Configuration["Auth0:ClientId"]);
                c.OAuthAppName("Swagger Api Calls");
                c.OAuthScopes(Configuration.GetSection("Auth0:Scopes").Get<string[]>());

                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.DocumentTitle = "miq-api";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                }
            });
        }

        //app.UseHttpsRedirection();

        // app.MapHealthChecks("health", new HealthCheckOptions
        // {
        //     ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        // });

        app.UseExceptionMiddleware();
        app.UseLoggingMiddleware();                

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseSerilogRequestLogging();
        // app.UseRequestContextMiddleware();

        app.MapControllers();
    }
}