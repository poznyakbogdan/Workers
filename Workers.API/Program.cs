using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.OpenApi.Models;
using MySql.EntityFrameworkCore.Extensions;
using Workers.Abstractions;
using Workers.API;
using Workers.API.Converters;
using Workers.API.Models.Input;
using Workers.API.Validation;
using Workers.DAL;
using Workers.DAL.Models;
using Workers.Implementations;
using AppContext = Workers.DAL.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDesignTimeDbContextFactory<AppContext>>(x =>
    new AppContextFactory(builder.Configuration.GetConnectionString("Workers"),
        x.GetService<ILoggerFactory>()));
builder.Services.AddScoped(x =>
    x.GetService<IDesignTimeDbContextFactory<AppContext>>().CreateDbContext(new[] { "" }));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();
    c.DescribeAllParametersInCamelCase();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkersApi", Version = "v1" });
});

builder.Services.AddScoped<IValidator<PostEmployee>, PostEmployeeValidator>();
builder.Services.AddScoped<IValidator<PostPosition>, PostPositionValidator>();
builder.Services.AddScoped<IValidator<PutPosition>, PutPositionValidator>();
builder.Services.AddScoped<IValidator<PutEmployee>, PutEmployeeValidator>();

builder.Services.AddScoped<IPositionsService, PositionsService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();

app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

app.MapControllers();

app.Run();