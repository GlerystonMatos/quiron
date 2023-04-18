using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quiron.Api.Configuracoes;
using Quiron.Api.Middleware;
using Quiron.CrossCutting;
using Quiron.Domain.Tenant;
using Quiron.Service.AutoMapper;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Quiron.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _name = configuration.GetValue<string>("Application:Name");
            _version = configuration.GetValue<string>("Application:Version");
        }

        public string _name { get; }

        public string _version { get; }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
                .AddOData(opt => opt.Select().Expand().Filter().OrderBy().SetMaxTop(100).Count()
                .AddRouteComponents("OData", EdmModelConfig.GetEdmModel()));

            services.AddJwtSetup();
            services.RegisterDependencies();
            services.AddSwaggerSetup(_name, _version);
            services.AddAutoMapper(typeof(AutoMapping));
            services.AddScoped<IAuthorizationHandler, TenantMiddleware>();
            services.Configure<TenantConfigurationSection>(_configuration);

            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256,
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseExceptionHandlerCuston();

            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.EnableFilter();
                ui.DocumentTitle = "Quiron";
                ui.DocExpansion(DocExpansion.None);
                ui.InjectStylesheet("/swagger-ui/custom.css");
                ui.SwaggerEndpoint("/swagger/v" + _version + "/swagger.json", _name + " V" + _version);
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}