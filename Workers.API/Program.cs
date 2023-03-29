using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySql.EntityFrameworkCore.Extensions;
using Workers.DAL.Models;
using AppContext = Workers.DAL.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<AppContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("Workers"));
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkersApi", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.MapGet("/", () => "Hello World!");

app.Run();