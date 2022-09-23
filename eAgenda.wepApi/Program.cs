using eAgenda.Infra.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace eAgenda.wepApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfiguracaoLogseAgenda.ConfigurarEscritaLogs();
            Log.Logger.Information("Iniciando a api eAgenda-Web...");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex,"O servidor da api eAgenda-Web parou inesperadamente...");
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {            
                    webBuilder.UseStartup<Startup>();
                });
    }
}
