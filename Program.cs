using LibraryAPI.Classes;
using LibraryAPI.Contracts;
using LibraryAPI.Filters;
using LibraryAPI.Managers;
using LibraryAPI.Middlewares;
using LibraryAPI.Repositories;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddXmlSerializerFormatters();
// Make [Consumes(application/xml)] case sensitive
builder.Services.AddControllersWithViews().
                AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>

    options.OperationFilter<TokenRequiredParameter>()
    
    );

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Default");

builder.Services.AddScoped<AuthorisationFilter>();
builder.Services.AddTransient<IDbConnection>((sp) => new MySqlConnection(connectionString));
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
builder.Services.AddScoped<IBooksManager, BooksManager>();
builder.Services.AddScoped<IAuthorsManager, AuthorsManager>();
builder.Services.AddHttpClient<IMyApiClient, MyApiClient>(client =>
{
    client.BaseAddress = new Uri("https://openlibrary.org/search.json");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<AuthorisationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();


