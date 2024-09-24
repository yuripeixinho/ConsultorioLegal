using CL.WebApi.Configuration;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{ambiente}.json", optional: true)  // Configura��o espec�fica de ambiente � opcional
    .AddEnvironmentVariables(); // Carregar tamb�m vari�veis de ambiente

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // L� a configura��o do appsettings.json
    .CreateLogger();

builder.Host.UseSerilog(); // Configura o ASP.NET Core para usar o Serilog como o logger principal

// Registra automaticamente todos os validadores da assembly espec�fica
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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction()) // Se for produ��o
{
    app.UseExceptionHandler("/error");
    // Podemos adicionar um logger espec�fico de produ��o aqui ou configura��es de seguran�a adicionais
    app.UseHsts(); // Garantir que o HSTS esteja habilitado para seguran�a
}

app.UseDatabaseConfiguration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



try
{
    Log.Information("Iniciando a WebApi");
    Log.Information($"Ambiente carregado: {ambiente}");

    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Erro catastr�fico.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}


