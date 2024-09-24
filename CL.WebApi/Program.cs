using CL.WebApi.Configuration;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{ambiente}.json", optional: true)  // Configuração específica de ambiente é opcional
    .AddEnvironmentVariables(); // Carregar também variáveis de ambiente

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Lê a configuração do appsettings.json
    .CreateLogger();

builder.Host.UseSerilog(); // Configura o ASP.NET Core para usar o Serilog como o logger principal

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
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction()) // Se for produção
{
    app.UseExceptionHandler("/error");
    // Podemos adicionar um logger específico de produção aqui ou configurações de segurança adicionais
    app.UseHsts(); // Garantir que o HSTS esteja habilitado para segurança
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
    Log.Fatal(ex, "Erro catastrófico.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}


