using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Quiron.Api.Filter;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;

namespace Quiron.Api.Configuracoes
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services, string name, string version)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v" + version, new OpenApiInfo
                {
                    Title = name,
                    Version = "v" + version,
                    Description = "Documentação da api do projeto Quiron.<br/>" +
                        "Para usar o OData nos endpoints onde o mesmo está disponível é necessário utilizar o prefixo “OData”.<br/>" +
                        "A autenticação da api deve ser feita enviando o usuário e a senha para o endpoint /Login/Authenticate.<br/>" +
                        "Em seguida o token deve ser enviado no Header da requisição: Authorization – Bearer Token."
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Por favor insira JWT com Bearer no campo",
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new string[] { }
                    }
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.SchemaFilter<SwaggerSchemaFilter>();
                s.DocumentFilter<SwaggerDocumentFilter>();

                s.CustomSchemaIds(x => (x.GetCustomAttributes<DisplayNameAttribute>().Count() > 0)
                    ? x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault().DisplayName
                    : x.Name);
            });

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata").MediaType);

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata").MediaType);
            });
        }
    }
}