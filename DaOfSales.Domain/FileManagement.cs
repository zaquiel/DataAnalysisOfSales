using DaOfSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaOfSales.Domain
{
    public class FileManagement: IFileManagement
    {        
        public FileManagement()
        {            
        }
            
        public List<string> Scanner(Configuration configuration)
        {
            var filesPathResult = new List<string>();

            try
            {
                var files = Directory.GetFiles(configuration.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

                if (files.Any())
                {
                    Console.WriteLine($"Files found: {files.Count}");

                    files.ForEach(x =>
                    {                
                        filesPathResult.Add(x);
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return filesPathResult;
        }

        public void SaveFile(SummaryResult summaryResult, Configuration configuration)
        {
            try
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Amount of clients: {summaryResult.AmoutClients}");
                stringBuilder.AppendLine($"Amount of salesman: {summaryResult.AmoutSalesman}");
                stringBuilder.AppendLine($"ID of the most expensive sale: {summaryResult.IdExpensiveSale}");
                stringBuilder.AppendLine($"Worst salesman ever: {summaryResult.WorstSalesman}");

                var destination = Path.Combine(configuration.RootPathOut, summaryResult.FileName);

                using (StreamWriter swriter = new StreamWriter(destination))
                {
                    swriter.Write(stringBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void MoveForGarbage(string filePath, Configuration configuration)
        {
            try
            {
                var destination = Path.Combine(configuration.RootPathGarbage, Path.GetFileName(filePath));
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }
                File.Move(filePath, destination);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
