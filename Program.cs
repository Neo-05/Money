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

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Money",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") //front
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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

//Sert � enregistrer des services dans le syst�me d'injection de d�pendance
//type �l� inject� et son impl�mentation concr�te
//permet de fournir une impl�mentation concr�te lorsqu'une instance d'une inter est cr�e
//Services
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

//Enregistre le AuthService dans le syst�me d'injection de d�pendance
builder.Services.AddScoped<AuthService>();

//Repo
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();


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

app.UseCors("Money");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
