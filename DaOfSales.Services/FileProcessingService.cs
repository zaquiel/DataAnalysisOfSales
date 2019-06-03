using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace DaOfSales.Services
{    
    public class FileProcessingService: IFileProcessingService
    {        
        private IFileManagement _fileManagement;
        private IFileProcessor _fileProcessor;        

        public Configuration Configuration { get; private set; }


        public FileProcessingService(IFileManagement fileManagement, 
            IFileProcessor fileProcessor)
        {
            LoadConfiguration();            
            _fileManagement = fileManagement;
            _fileProcessor = fileProcessor;
        }

        private void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Configuration = new Configuration
            {
                RootPathIn = configuration?.GetSection("Paths")?.GetSection("RootPathIn")?.Value ?? "",
                RootPathOut = configuration?.GetSection("Paths")?.GetSection("RootPathOut")?.Value ?? "",
                RootPathGarbage = configuration?.GetSection("Paths")?.GetSection("RootPathGarbage")?.Value ?? ""
            };

            if (!Directory.Exists(Configuration.RootPathIn))
            {
                Directory.CreateDirectory(Configuration.RootPathIn);
            }

            Console.WriteLine($"Directory In: {Configuration.RootPathIn}");

            if (!Directory.Exists(Configuration.RootPathOut))
            {
                Directory.CreateDirectory(Configuration.RootPathOut);
            }

            Console.WriteLine($"Directory Out: {Configuration.RootPathOut}");

            if (!Directory.Exists(Configuration.RootPathGarbage))
            {
                Directory.CreateDirectory(Configuration.RootPathGarbage);
            }

            Console.WriteLine($"Directory Garbage: {Configuration.RootPathGarbage}");
        }

        public void ProcessFiles()
        {
            try
            {
                var files = _fileManagement.Scanner(Configuration);

                files.ForEach(x =>
                {                    
                    var summaryResult = _fileProcessor.SummarizeFile(x);

                    if (summaryResult != null)
                    {
                        _fileManagement.SaveFile(summaryResult, Configuration);                        
                    }
                    _fileManagement.MoveForGarbage(x, Configuration);
                });            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
