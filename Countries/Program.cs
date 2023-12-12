using Countries.BusinessLogicLayer.Interfaces;
using Countries.BusinessLogicLayer.Service;
using Countries.DataLayer;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// response caching
builder.Services.AddResponseCaching();

// Add Output Caching
builder.Services.AddOutputCache();

builder.Services.Configure<ConfigurationOptions>(
        builder.Configuration.GetSection("RedisCacheOptions"));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
    options.InstanceName = "CountriesAPI";

});

builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlServer")));
builder.Services.AddTransient<ICountryInterface, CountryService>();
builder.Services.AddTransient<IRedisService, RedisService>();

builder.Services.AddTransient<ICountryInterface, CountryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


// response caching
//app.UseResponseCaching();

// Add Output Caching
app.UseOutputCache();

app.MapControllers();

app.Run();
