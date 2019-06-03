using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace DaOfSales.Test
{
    public class FileProcessorTest: BaseTest
    {
        private FileProcessor _fileProcessor;
        public FileProcessorTest()
        {
            _fileProcessor = new FileProcessor();

            base.Initialize();
        }
       
        [Fact]
        public void ShouldSummarizingCorrectTheFileIfExists()
        {
            var filePath = Directory.GetFiles(@".\DataIn\", "File_Processor_Test.20190103.dat", SearchOption.AllDirectories).FirstOrDefault();

            var summaryResult = new SummaryResult
            {
                AmoutClients = 2,
                AmoutSalesman = 2,
                IdExpensiveSale = "10",
                WorstSalesman= "Renato",
                FileName = "File_Processor_Test.20190103.done.dat"
            };

            var summaryResponse = _fileProcessor.SummarizeFile(filePath);

            summaryResponse.Should().Be(summaryResult);

        }

        [Fact]
        public void ShouldNotSummarizingTheFileIfNotExists()
        {
            var filePath = Directory.GetFiles(@".\DataIn\", "12121221212___xxxxxxx.dat", SearchOption.AllDirectories).FirstOrDefault();

            var summaryResponse = _fileProcessor.SummarizeFile(filePath);

            summaryResponse.Should().BeNull();
        }
    }
}
