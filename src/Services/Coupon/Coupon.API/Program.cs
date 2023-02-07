using Autofac.Extensions.DependencyInjection;
using Coupon.API.Infrastructure.Filters;
using Coupon.API.Infrastructure.Services;
using Coupon.Domain.EventHandlers;
using Coupon.Domain.Events;
using Coupon.Domain.Interfaces;
using Coupon.Infrastructure.Configuration;
using Coupon.Infrastructure.Repositories;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = GetConfiguration();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "eShopOnContainers - Coupon HTTP API",
        Version = "v1",
        Description = "The Coupon Service HTTP API"
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/authorize"),
                TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrl")}/connect/token"),
                Scopes = new Dictionary<string, string>()
                {
                    { "coupon", "Coupon API" }
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowCors", x =>
    {
        x
        .WithOrigins("http://webspa.my:8080")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddRabbitMQPersistentConnection(configuration);
builder.Services.AddEventBus(configuration);
builder.Services.AddCustomAuthentication(configuration);
builder.Services.AddCustomIntegrations(configuration);
builder.Services.AddBussinesServices();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<ICouponRepository, CouponRepository>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI(setup =>
        {
            var pathBase = configuration["PATH_BASE"];
            setup.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "Coupon.API V1");
            setup.OAuthClientId("couponswaggerui");
            setup.OAuthAppName("Coupon Swagger UI");
        });
}
if (!string.IsNullOrEmpty(configuration["PATH_BASE"]))
{
    app.UsePathBase(configuration["PATH_BASE"]);
}
app.Use(async (ctx, next) =>
{
    Console.WriteLine(ctx.Request);
    Console.WriteLine(ctx.Request.Path);
    await next(ctx);
});
ConfigureUseEvens(app);

app.UseRouting();
app.UseCors("AllowCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


IConfiguration GetConfiguration()
{
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
}

void ConfigureUseEvens(WebApplication app)
{
    var eventBus = app.Services.GetRequiredService<IEventBus>();
    eventBus.Subscribe<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent, OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler>();
    eventBus.Subscribe<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
    eventBus.Subscribe<OrderStatusChangedToAwaitingPointsValidationIntegrationEvent, OrderStatusChangedToAwaitingPointsValidationIntegrationEventHandler>();
}
