using CL.Manager.Validator;
using FluentValidation.AspNetCore;
using Newtonsoft.Json;

namespace CL.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .AddFluentValidation(p =>
            {
                p.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>();
                p.RegisterValidatorsFromAssemblyContaining<AlteraClienteValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovoEnderecoValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NovoTelefoneValidator>();
                p.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("pt-br");
            });

        }
    }
}
