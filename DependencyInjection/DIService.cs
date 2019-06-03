using DaOfSales.Domain;
using DaOfSales.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection
{
    public static class DIService
    {
        private static ServiceProvider _provider;
        private static ServiceProvider ServiceProvider
        {
            get
            {
                if (_provider == null)
                {
                    var services = CreateServices();

                    _provider = services.BuildServiceProvider();
                }

                return _provider;
            }
        }

        public static T GetService<T>() where T : class
        {
            return ServiceProvider.GetService<T>();
        }

        private static ServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IFileProcessingService, FileProcessingService>();            
            services.AddTransient<IFileManagement, FileManagement>();
            services.AddTransient<IFileProcessor, FileProcessor>();            

            return services;
        }
    }
}
