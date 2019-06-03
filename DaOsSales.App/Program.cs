using DaOfSales.Services;
using DependencyInjection;
using System;
using System.IO;

namespace DaOfSales.App
{
    class Program
    {                
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing application");            

            var fileProcessingService = DIService.GetService<IFileProcessingService>();

            while (true)
            {
                try
                {
                    fileProcessingService.ProcessFiles();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
