using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Orders.Api;
using Orders.Api.Interfaces;
using Orders.Api.Managers;
using Orders.Data;
using Orders.Data.Interfaces;
using Orders.Data.Repositories;
using System.Text.Json.Serialization;

// initialization WebApplication builder
var builder = WebApplication.CreateBuilder(args);

//retrieve the connection string for the db from configuration file
var connectionString = builder.Configuration.GetConnectionString("LocalOrdersConnection");

//configure services for DI and db connection 
builder.Services.AddDbContext<OrdersDbContext>(options =>
	options.UseSqlServer(connectionString)                  //configure sql server as db provider
		.UseLazyLoadingProxies(false)                       // unenable Lazy Loading for navigation properties
		.ConfigureWarnings(x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))); //suppress warnings for Lazy loading on disposed context

// add support for controllers and configure JSON serialization options
builder.Services.AddControllers().AddJsonOptions(options => 
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// enable API andpoint exploration 
builder.Services.AddEndpointsApiExplorer();

//register repository for DI , to manage db entities
builder.Services.AddScoped<IOrderRepository, orderRepository>();

//register business logic managers for DI, and background manager
builder.Services.AddScoped<IOrderManager, OrderManager>();

//in-memory queue for storing and retrieving payment information
builder.Services.AddSingleton<PaymentQueueManager>();
//continuously processes payment information from the queue
builder.Services.AddHostedService<PaymentProcessingManager>();

//configure AutoMapper and specify the configure profile
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));

//build the app
var app = builder.Build();

//default endpoint for the root URL 
app.MapGet("/", () => "Hello Orders!");

//map controller routes to their endpoints
app.MapControllers();

//run the app
app.Run();
