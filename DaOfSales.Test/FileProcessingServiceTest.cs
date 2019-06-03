using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using DaOfSales.Services;
using FluentAssertions;
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

        public FileProcessingServiceTest()
        {
            _fileManagementMock = new Mock<IFileManagement>();
            _fileProcessorMock = new Mock<IFileProcessor>();

            _fileProcessingService = new FileProcessingService(_fileManagementMock.Object, _fileProcessorMock.Object);

            base.Initialize();

        }

        [Fact]
        public void ShouldEnsureThatTheDirectoriesAreCreated()
        {
            var fileProcessingService = new FileProcessingService(_fileManagementMock.Object, _fileProcessorMock.Object);

            Directory.Exists(fileProcessingService.Configuration.RootPathIn).Should().BeTrue();
            Directory.Exists(fileProcessingService.Configuration.RootPathOut).Should().BeTrue();
            Directory.Exists(fileProcessingService.Configuration.RootPathGarbage).Should().BeTrue();
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

            var files = _fileManagementMock.Setup(x => x.Scanner(It.IsAny<Configuration>())).Returns(filesTest);

            var response = _fileProcessorMock.Setup(x => x.SummarizeFile(filesTest[0])).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()));
            _fileManagementMock.Setup(x => x.MoveForGarbage(filesTest[0], It.IsAny<Configuration>()));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(It.IsAny<Configuration>()), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(filesTest[0]), Times.Once);

            _fileManagementMock.Verify(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()), Times.Once);
            _fileManagementMock.Verify(x => x.MoveForGarbage(filesTest[0], It.IsAny<Configuration>()), Times.Once);
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

            var files = _fileManagementMock.Setup(x => x.Scanner(It.IsAny<Configuration>())).Returns(filesTest);

            var response = _fileProcessorMock.Setup(x => x.SummarizeFile(It.IsAny<string>())).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()));
            _fileManagementMock.Setup(x => x.MoveForGarbage(It.IsAny<string>(), It.IsAny<Configuration>()));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(It.IsAny<Configuration>()), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(It.IsAny<string>()), Times.Never);

            _fileManagementMock.Verify(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()), Times.Never);
            _fileManagementMock.Verify(x => x.MoveForGarbage(It.IsAny<string>(), It.IsAny<Configuration>()), Times.Never);
        }

        [Fact]
        public void ShouldNotProcessEmptyFiles()
        {
            var filesTest = new List<string>
            {
                "empty.dat"
            };

            SummaryResult summaryResult = null;

            var files = _fileManagementMock.Setup(x => x.Scanner(It.IsAny<Configuration>())).Returns(filesTest);

            var response = _fileProcessorMock.Setup(x => x.SummarizeFile(It.IsAny<string>())).Returns(summaryResult);

            _fileManagementMock.Setup(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()));
            _fileManagementMock.Setup(x => x.MoveForGarbage(It.IsAny<string>(), It.IsAny<Configuration>()));

            _fileProcessingService.ProcessFiles();

            _fileManagementMock.Verify(x => x.Scanner(It.IsAny<Configuration>()), Times.Once);
            _fileProcessorMock.Verify(x => x.SummarizeFile(It.IsAny<string>()), Times.Once);

            _fileManagementMock.Verify(x => x.SaveFile(summaryResult, It.IsAny<Configuration>()), Times.Never);
            _fileManagementMock.Verify(x => x.MoveForGarbage(It.IsAny<string>(), It.IsAny<Configuration>()), Times.Once);
        }
    }
}
