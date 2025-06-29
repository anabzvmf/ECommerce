using Infra.ControlesAcessos;
using Model.ControleAcessos;
using Microsoft.AspNetCore.Identity;
using Backend.Endpoints;
using Infra.Produtos;
using Backend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    //x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.Instancia.ChavePrivada)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorizationBuilder();
//.AddPolicy("Admin", policy => policy.RequireRole("admin"))
//.AddPolicy("Gerente", policy => policy.RequireRole("gerente"))
//.AddPolicy("AdminOuGerente", policy => policy.RequireClaim(ClaimTypes.Role, "admin", "gerente"));

//Adiciona o Distributed Memory Cache, que � um cache em mem�ria que pode ser substitu�do no futuro por um
//cache distribu�do, como Redis ou SQL Server.
builder.Services.AddDistributedMemoryCache();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.InjetarDependencias();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Comentado para evitar erro de redirecionamento HTTPS em dev

app.AdicionarTodosEndpoints();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

app.Run();