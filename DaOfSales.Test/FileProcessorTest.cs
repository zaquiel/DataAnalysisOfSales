using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
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
        private readonly Mock<ILogger<FileProcessor>> _logger;
        private readonly Mock<IDataParserHelper> _dataParserHelper;
        private FileProcessor _fileProcessor;
        public FileProcessorTest()
        {
            _logger = new Mock<ILogger<FileProcessor>>();
            _dataParserHelper = new Mock<IDataParserHelper>();

            _fileProcessor = new FileProcessor(_logger.Object, _dataParserHelper.Object);

            base.Initialize();
        }
       
        [Fact]
        public void ShouldSummarizingCorrectTheFileIfExists()
        {
            var filePath = Directory.GetFiles(PathConfigurations.RootPathIn, 
                "File_Processor_Test.20190103.dat", SearchOption.AllDirectories).FirstOrDefault();

            var abstractLine1 = base.ParserHelper("001ç1234567891234çDiegoç50000");
            var abstractLine2 = base.ParserHelper("001ç3245678865434çRenatoç40000.99");
            var abstractLine3 = base.ParserHelper("002ç2345675434544345çJosedaSilvaçRural");
            var abstractLine4 = base.ParserHelper("002ç2345675433444345çEduardoPereiraçRural");
            var abstractLine5 = base.ParserHelper("003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çDiego");
            var abstractLine6 = base.ParserHelper("003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çRenato");        

            _dataParserHelper.Setup(x => x.Parser("001ç1234567891234çDiegoç50000")).Returns(abstractLine1);
            _dataParserHelper.Setup(x => x.Parser("001ç3245678865434çRenatoç40000.99")).Returns(abstractLine2);
            _dataParserHelper.Setup(x => x.Parser("002ç2345675434544345çJosedaSilvaçRural")).Returns(abstractLine3);
            _dataParserHelper.Setup(x => x.Parser("002ç2345675433444345çEduardoPereiraçRural")).Returns(abstractLine4);
            _dataParserHelper.Setup(x => x.Parser("003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çDiego")).Returns(abstractLine5);
            _dataParserHelper.Setup(x => x.Parser("003ç08ç[1-34-10,2-33-1.50,3-40-0.10]çRenato")).Returns(abstractLine6);

            var summaryResult = new SummaryResult
            {
                AmoutClients = 2,
                AmoutSalesman = 2,
                IdExpensiveSale = "10",
                WorstSalesman = "Renato",
                FileName = "File_Processor_Test.20190103.done.dat"
            };

            var summaryResponse = _fileProcessor.SummarizeFile(filePath);

            summaryResponse.Should().Be(summaryResult);

        }

        [Fact]
        public void ShouldNotSummarizingTheFileIfNotExists()
        {
            var filePath = Directory.GetFiles(PathConfigurations.RootPathIn,
             "12121221212___xxxxxxx.dat", SearchOption.AllDirectories).FirstOrDefault();

            var summaryResponse = _fileProcessor.SummarizeFile(filePath);

            summaryResponse.Should().BeNull();
        }
    }
}
