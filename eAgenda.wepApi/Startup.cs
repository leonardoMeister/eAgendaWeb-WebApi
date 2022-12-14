using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.wepApi.Controllers.Config.AutoMapperConfig;
using eAgenda.wepApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace eAgenda.wepApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });

            //Injecao de dependencias
            services.AddAutoMapper(config =>
            {
                config.AddProfile<TarefaProfile>();
                config.AddProfile<ContatoProfile>();
                config.AddProfile<CompromissoProfile>();
                config.AddProfile<DespesaProfile>();
                config.AddProfile<CategoriaProfile>();
            });

            //adicionando configs da aplicacao
            services.AddSingleton(x => new ConfiguracaoAplicacaoeAgenda().ConnectionStrings);
            services.AddScoped<IContextoPersistencia, eAgendaDbContext>();

            //Injection dos modulos app
            services.AddScoped<IRepositorioTarefa, RepositorioTarefaOrm>();
            services.AddTransient<ServicoTarefa>();

            services.AddScoped<IRepositorioContato, RepositorioContatoOrm>();
            services.AddTransient<ServicoContato>();

            services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoOrm>();
            services.AddTransient<ServicoCompromisso>();

            services.AddScoped<IRepositorioDespesa, RepositorioDespesaOrm>();
            services.AddTransient<ServicoDespesa>();

            services.AddScoped<IRepositorioCategoria, RepositorioCategoriaOrm>();
            services.AddTransient<ServicoCategoria>();

            //Adicionando os filtros
            services.AddControllers(config =>
            {
                config.Filters.Add<ValidarViewModelActionFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eAgenda.wepApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eAgenda.wepApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
