using CL.Data.Context;
using CL.WebApi.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registra automaticamente todos os validadores da assembly específica
builder.Services.AddControllers();
builder.Services.AddFluentValidationConfiguration();

builder.Services.AddAutomapperConfiguration();

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddDependencyInjectionConfiguration(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration(); //builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDatabaseConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
