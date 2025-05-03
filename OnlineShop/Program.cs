using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Features;
using OnlineShop.Middlewares;
using OnlineShop.Repositories;
using OnlineShop.Services;

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
app.Run();
