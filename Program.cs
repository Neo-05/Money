using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Money.BLL.Interfaces;
using Money.BLL.Services;
using Money.DAL.Interfaces;
using Money.DAL.Repositories;
using MoneyApi.Models;
using System.Data.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Token JWT
//R�cup�re les info de appsettings et stock ds la class JwtOptions
JwtOptions options = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

//Injecter le jwtoption
builder.Services.AddSingleton(options);

//Configuration de l'auth ds les services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        o =>
        {
            //chercher cl�
            byte[] sKey = Encoding.UTF8.GetBytes(options.SigningKey);

            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(sKey)
            };
        }
    );
builder.Services.AddAuthorization();

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
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Repo
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure l�application pour qu�elle cr�e une nouvelle instance de SqlConnection avec la cha�ne de connexion �Default� chaque fois qu�une DbConnection est n�cessaire.
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
