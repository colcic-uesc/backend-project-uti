using Microsoft.EntityFrameworkCore;
using UescColcicAPI.Services.BD;
using UescColcicAPI.Services.BD.Interfaces;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
