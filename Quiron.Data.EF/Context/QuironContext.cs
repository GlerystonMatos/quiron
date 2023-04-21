using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quiron.Auditoria.Interceptor;
using Quiron.Data.EF.Configuration;
using Quiron.Domain.Entities;
using Quiron.Domain.Interfaces.Services;
using Quiron.Domain.Tenant;
using System;
using System.Collections.Generic;

namespace Quiron.Data.EF.Context
{
    public class QuironContext : DbContext
    {
        private readonly ITenantService _tenantService;
        private readonly ILogger<QuironContext> _logger;
        private AuditoriaInterceptor _auditoriaInterceptor;

        public QuironContext(DbContextOptions<QuironContext> options, ITenantService tenantService, ILogger<QuironContext> logger) : base(options)
        {
            _logger = logger;
            _tenantService = tenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AnimalConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
            modelBuilder.ApplyConfiguration(new CidadeConfig());
            modelBuilder.ApplyConfiguration(new UsuarioConfig());

            IList<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(new Usuario(Guid.Parse("d78a657f-66fa-43f2-a535-212e6bfb6630"), "Teste 01", "Teste01", "1234"));
            usuarios.Add(new Usuario(Guid.Parse("10b42acc-45bd-460a-9edd-d568ff236e37"), "Teste 02", "Teste02", "4567"));

            modelBuilder.Entity<Usuario>().HasData(usuarios);

            IList<Animal> animais = new List<Animal>();
            animais.Add(new Animal(Guid.Parse("1dfc4a8d-7ed1-443c-9cc7-ac71ea9d003b"), "Cachorro"));
            animais.Add(new Animal(Guid.Parse("8b5c8482-f2ec-4cf6-aaa8-20ec25112cd7"), "Hamster"));

            modelBuilder.Entity<Animal>().HasData(animais);

            Estado ceara = new Estado(Guid.Parse("362c52b3-b9db-4aca-a48f-6e47aa77f819"), "Ceará", "CE");
            Estado rioGrandeNorte = new Estado(Guid.Parse("c4a41075-59a0-4e87-8a1c-0d542bc90155"), "Rio Grande do Norte", "RN");

            IList<Estado> estados = new List<Estado>();
            estados.Add(ceara);
            estados.Add(rioGrandeNorte);

            modelBuilder.Entity<Estado>().HasData(estados);

            IList<Cidade> cidades = new List<Cidade>();
            cidades.Add(new Cidade(Guid.Parse("373fad00-4ace-4c53-abbd-4fa11212cd88"), "Fortaleza", ceara.Id));
            cidades.Add(new Cidade(Guid.Parse("a5dc78eb-d526-42e1-bf5d-ba8a571a8b69"), "Caucaia", ceara.Id));
            cidades.Add(new Cidade(Guid.Parse("e374123a-423f-45b3-994f-68065e291f9d"), "Maracanaú", ceara.Id));
            cidades.Add(new Cidade(Guid.Parse("70604862-7558-4b46-b6e1-787f6a20eb7c"), "Natal", rioGrandeNorte.Id));
            cidades.Add(new Cidade(Guid.Parse("1d6209ec-a2d3-4536-8568-ee58cd8c46aa"), "Pipa", rioGrandeNorte.Id));

            modelBuilder.Entity<Cidade>().HasData(cidades);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            TenantConfiguration configuration = _tenantService.Get();
            if (configuration != null)
            {
                _auditoriaInterceptor = new AuditoriaInterceptor(configuration.ConnectionStringAuditoria, _tenantService.GetUser());

                builder.AddInterceptors(_auditoriaInterceptor);
                builder.UseSqlServer(configuration.ConnectionStringDados);

                _logger.LogInformation("ConnectionStringDados: " + configuration.ConnectionStringDados);
                _logger.LogInformation("ConnectionStringAuditoria: " + configuration.ConnectionStringAuditoria);
            }
        }
    }
}