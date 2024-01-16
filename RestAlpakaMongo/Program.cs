using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

using Microsoft.OpenApi.Models;
using RestAlpakaMongo.Interfaces;
using RestAlpakaMongo.MongoDBInitializer;
using RestAlpakaMongo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alpaka API", Version = "v1" });
});

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
{
    var uri = builder.Configuration["ConnectionStrings:DefaultConnection"];
    return new MongoClient(uri);
});

builder.Services.AddSingleton<AlpakaService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddSingleton<CustomersService>();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<LocationService>();
builder.Services.AddSingleton<PaymentsService>();
builder.Services.AddSingleton<ReviewsService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<TicketsService>(); 

var app = builder.Build();
var dbInitializer = app.Services.GetService<DatabaseInitializer>();
dbInitializer?.Initialize();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();