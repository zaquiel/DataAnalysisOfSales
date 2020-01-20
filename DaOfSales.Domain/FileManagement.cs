using DaOfSales.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaOfSales.Domain
{
    public class FileManagement: IFileManagement
    {        
        private readonly IOptions<PathConfigurations> _pathConfigurations;
        private readonly ILogger<FileManagement> _logger;
        
        public FileManagement(ILogger<FileManagement> logger,
            IOptions<PathConfigurations> pathConfigurations)
        {            
            _logger = logger;
            _pathConfigurations = pathConfigurations;
        }
            
        public IEnumerable<string> Scanner()
        {      
            string[] files = null;
            try
            {                
                files = Directory.GetFiles(_pathConfigurations.Value.RootPathIn, "*.dat", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }            

            if ((files != null) && (files.Count() > 0))
            {
                Console.WriteLine($"Files found: {files.Count()}");

                foreach (var file in files)
                {
                    yield return file;
                }
            }            
        }

        public void SaveFile(SummaryResult summaryResult)
        {
            try
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Amount of clients: {summaryResult.AmoutClients}");
                stringBuilder.AppendLine($"Amount of salesman: {summaryResult.AmoutSalesman}");
                stringBuilder.AppendLine($"ID of the most expensive sale: {summaryResult.IdExpensiveSale}");
                stringBuilder.AppendLine($"Worst salesman ever: {summaryResult.WorstSalesman}");

                var destination = Path.Combine(_pathConfigurations.Value.RootPathOut, summaryResult.FileName);

                using (StreamWriter swriter = new StreamWriter(destination))
                {
                    swriter.Write(stringBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public string MoveForProcessing(string filePath)
        {
            string destination = string.Empty;
            try
            {
                destination = Path.Combine(_pathConfigurations.Value.RootPathProcessing, Path.GetFileName(filePath));
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                File.Move(filePath, destination);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return destination;
        }
    }
}
