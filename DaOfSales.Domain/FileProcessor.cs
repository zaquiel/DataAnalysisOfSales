using DaOfSales.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DaOfSales.Domain
{
    public class FileProcessor: IFileProcessor
    {        
        private IDataParserHelper _dataParserHelper;
        private ILogger<FileProcessor> _logger;

        public FileProcessor(ILogger<FileProcessor> logger,
            IDataParserHelper dataParserHelper)
        {            
            _dataParserHelper = dataParserHelper;
            _logger = logger;
        }

        public SummaryResult SummarizeFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var entities = new List<AbstractEntity>();

                    using (StreamReader file = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = file.ReadLine()) != null)
                        {
                            var fileProcessed = _dataParserHelper.Parser(line);
                            entities.Add(fileProcessed);
                        }

                        file.Close();
                    }

                    if (entities.Any())
                    {
                        var summaryResult = new SummaryResult
                        {
                            FileName = $"{Path.GetFileNameWithoutExtension(filePath)}.done.dat",
                            AmoutSalesman = entities.OfType<Salesman>().Count(),
                            AmoutClients = entities.OfType<Customer>().Count(),
                            IdExpensiveSale = entities.OfType<Sales>().OrderByDescending(x => x.Items.Sum(y => y.Price)).FirstOrDefault().SalesId,
                            WorstSalesman = entities.OfType<Sales>().OrderBy(x => x.Items.Sum(y => y.Price)).FirstOrDefault().SalesmanName
                        };

                        return summaryResult;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return null;
        }
    }
}
