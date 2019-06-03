using DaOfSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaOfSales.Domain
{
    public class FileProcessor: IFileProcessor
    {        
        private DataParserHelper _dataParserHelper;

        public FileProcessor()
        {            
            _dataParserHelper = new DataParserHelper();
        }

        public SummaryResult SummarizeFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    List<AbstractEntity> entities = new List<AbstractEntity>();

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
                    Console.WriteLine(ex);
                }
            }

            return null;
        }
    }
}
