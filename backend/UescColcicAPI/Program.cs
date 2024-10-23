using Microsoft.EntityFrameworkCore;
using UescColcicAPI.Services.BD;
using UescColcicAPI.Services.BD.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UescColcicAPI.Middlewares;
using UescColcicAPI.Services.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "colcic.uesc.br",
        ValidAudience = "colcic.uesc.br",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ChaveSecretaColcicComMaisDe16Caracteres"))
    };
});

// Adicionar autorização
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddDbContext<UescColcicAPIDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("UescColcicDb");
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddScoped<IStudentsCRUD, StudentsCRUD>();
builder.Services.AddScoped<IProfessorsCRUD, ProfessorsCRUD>();
builder.Services.AddScoped<IProjectsCRUD, ProjectsCRUD>();
builder.Services.AddScoped<ISkillsCRUD, SkillsCRUD>();
builder.Services.AddScoped<AuthService>();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware de autenticação JWT
app.UseAuthentication();

// Middleware para log de eventos
app.UseMiddleware<EventLogMiddleware>();

// Middleware para adicionar cabeçalhos à resposta
app.UseMiddleware<ResponseAppendMiddleware>();

// Middleware de autorização (JWT)
app.UseAuthorization();

// Mapeamento dos controllers
app.MapControllers();

app.Run();
