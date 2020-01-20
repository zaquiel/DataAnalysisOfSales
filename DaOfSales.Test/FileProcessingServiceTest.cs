using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using DaOfSales.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace DaOfSales.Test
{

    public class FileProcessingServiceTest: BaseTest
    {
        private readonly IFileProcessingService _fileProcessingService;
        private readonly Mock<IFileManagement> _fileManagementMock;
        private readonly Mock<IFileProcessor> _fileProcessorMock;
        private readonly Mock<ILogger<FileProcessingService>> _logger;

        public FileProcessingServiceTest()
        {
            _fileManagementMock = new Mock<IFileManagement>();
            _fileProcessorMock = new Mock<IFileProcessor>();
            _logger = new Mock<ILogger<FileProcessingService>>();

            _fileProcessingService = new FileProcessingService(_logger.Object, 
                _fileManagementMock.Object, _fileProcessorMock.Object);

            base.Initialize();

        }

        [Fact]
        public void ShouldProcessTheFiles()
        {            
            var filesTest = new List<string>
            {
                "Test.dat"
            };

            var summaryResult = new SummaryResult
            {
                AmoutClients = 1,
                AmoutSalesman = 1,
                FileName = "Test.dat",
                IdExpensiveSale = "1a",
                WorstSalesman = "fulano"
            };

            var fileProcessing = Path.Combine(PathConfigurations.RootPathProcessing,filesTest[0]);

            _fileManagementMock.Setup(x => x.Scanner()).Returns(filesTest);

            _fileManagementMock.Setup(x => x.MoveForProcessing(filesTest[0])).Returns(fileProcessing);

            var response = _fileProcessorMock.Setup(x => x.SummarizeFile(fileProcessing)).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(), Times.Once);
            _fileManagementMock.Verify(x => x.MoveForProcessing(filesTest[0]), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(fileProcessing), Times.Once);
            _fileManagementMock.Verify(x => x.SaveFile(summaryResult), Times.Once);
        }

        [Fact]
        public void ShouldNotProcessWhenNoFiles()
        {
            var filesTest = new List<string>();

            var summaryResult = new SummaryResult
            {
                AmoutClients = 1,
                AmoutSalesman = 1,
                FileName = "Test.dat",
                IdExpensiveSale = "1a",
                WorstSalesman = "fulano"
            };

            _fileManagementMock.Setup(x => x.Scanner()).Returns(filesTest);

            _fileProcessorMock.Setup(x => x.SummarizeFile(It.IsAny<string>())).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult));
            _fileManagementMock.Setup(x => x.MoveForProcessing(It.IsAny<string>()));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(It.IsAny<string>()), Times.Never);

            _fileManagementMock.Verify(x => x.SaveFile(summaryResult), Times.Never);
            _fileManagementMock.Verify(x => x.MoveForProcessing(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ShouldNotProcessEmptyFiles()
        {
            var filesTest = new List<string>
            {
                "empty.dat"
            };

            SummaryResult summaryResult = null;

            _fileManagementMock.Setup(x => x.Scanner()).Returns(filesTest);

            _fileProcessorMock.Setup(x => x.SummarizeFile(It.IsAny<string>())).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult));
            _fileManagementMock.Setup(x => x.MoveForProcessing(It.IsAny<string>()));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(It.IsAny<string>()), Times.Once);

            _fileManagementMock.Verify(x => x.SaveFile(summaryResult), Times.Never);
            _fileManagementMock.Verify(x => x.MoveForProcessing(It.IsAny<string>()), Times.Once);
        }
    }
}
