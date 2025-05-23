using FluentValidation;
using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop;
using OnlineShop.Attributes;
using OnlineShop.Behaviors;
using OnlineShop.Data;
using OnlineShop.Features;
using OnlineShop.Middlewares;
using OnlineShop.Proxies;
using OnlineShop.Repositories;
using OnlineShop.Services;
using OnlineShop.ViewModels;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserEntityRepository,UserEntityRepository>();
builder.Services.AddScoped<ICityRepository,CityRepository>();

builder.Services.AddScoped<IUserEntityService, UserEntityService>();

builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddMemoryCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.MapGet("/Citys", async (IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
{
    var entitites = await unitOfWork.CityRepository.GetListCityAsync(cancellationToken);
    return BaseResult.Success(entitites);
})
    .WithTags("City");

var enumTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(x => x.GetTypes())
    .Where(t => t.IsEnum && t.GetCustomAttribute<EnumInfoAttribute>() != null);

foreach (var enumType in enumTypes)
{
    var route = enumType.GetCustomAttribute<EnumInfoAttribute>()!.Route;

    app.MapGet(route, () =>
    {
        var items = Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(e =>
            {
                var memberInfo = enumType.GetMember(e.ToString()!)[0];

               

                var infoAttributes = memberInfo
                    .GetCustomAttributes<InfoAttribute>()
                    .ToDictionary(a => a.Key, a => a.Value);

                //dynamic object
                return new 
                {
                    key = Convert.ToInt32(e),
                    value = e.ToString(),
                    Description = e.Humanize(),
                    information = infoAttributes,
                    
                   
                };
            });

        return Results.Ok(new { value = items });
    })
        .WithTags("Enums");
}

app.Run();
