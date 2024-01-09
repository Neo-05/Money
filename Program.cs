using Microsoft.Data.SqlClient;
using Money.BLL.Interfaces;
using Money.BLL.Services;
using Money.DAL.Interfaces;
using Money.DAL.Repositories;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//A REVOIR
//DbConnection
builder.Services.AddTransient<DbConnection>(service =>
{
    string connectionString = builder.Configuration.GetConnectionString("Default");
    return new SqlConnection(connectionString);
});

//Services
builder.Services.AddScoped<IPeopleService, PeopleService>();

//Repo
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure l’application pour qu’elle crée une nouvelle instance de SqlConnection avec la chaîne de connexion “Default” chaque fois qu’une DbConnection est nécessaire.
builder.Services.AddTransient<DbConnection>(service =>
{
    string connectionString = builder.Configuration.GetConnectionString("Default");
    return new SqlConnection(connectionString);
});

var app = builder.Build();

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
