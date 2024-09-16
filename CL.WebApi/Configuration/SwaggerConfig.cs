using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CL.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Consultório Legal",
                    Version = "v1",
                    Description = "API da aplicação consúltório legal",
                    Contact = new OpenApiContact
                    {
                        Name = "Yuri Peixinho",
                        Email = "yuripeixinho03@gmail.com",
                        Url = new Uri("https://github.com/yuripeixinho")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licença",
                        Url = new Uri("https://opensource/osd")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "CL.Core.Shared.xml");
                c.IncludeXmlComments(xmlPath);

            });
        }
    }
}
