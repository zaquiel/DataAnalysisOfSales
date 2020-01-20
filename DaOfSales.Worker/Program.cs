using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using DaOfSales.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DaOfSales.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) => 
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })                
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();               
                    services.Configure<PathConfigurations>(hostContext.Configuration.GetSection("PathConfigurations"));

                    services.AddTransient<IFileProcessingService, FileProcessingService>();            
                    services.AddTransient<IFileManagement, FileManagement>();
                    services.AddTransient<IFileProcessor, FileProcessor>();
                    services.AddTransient<IDataParserHelper, DataParserHelper>();

                    services.AddHostedService<Worker>();
                });
    }
}
