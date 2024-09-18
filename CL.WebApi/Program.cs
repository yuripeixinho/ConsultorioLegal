using CL.WebApi.Configuration;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Lê a configuração do appsettings.json
    .CreateLogger();

// Registra automaticamente todos os validadores da assembly específica
builder.Services.AddControllers();
builder.Services.AddFluentValidationConfiguration();

builder.Services.AddAutomapperConfiguration();

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration(); //builder.Services.AddSwaggerGen();

builder.Host.UseSerilog(); // Configura o ASP.NET Core para usar o Serilog como o logger principal

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


string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

try
{
    Log.Information("Iniciando a WebApi");
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Erro catastrófico.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}


