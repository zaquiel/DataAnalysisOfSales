using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IO;
using System.Linq;
using Xunit;

namespace DaOfSales.Test
{
    public class FileManagementTest: BaseTest
    {
        private readonly IFileManagement _fileManagement;

        private readonly Mock<IOptions<PathConfigurations>> _pathConfigurationsMock;
        private readonly Mock<ILogger<FileManagement>> _logger;

        public FileManagementTest()
        {
            _logger = new Mock<ILogger<FileManagement>>();
            _pathConfigurationsMock = new Mock<IOptions<PathConfigurations>>();            

            _fileManagement = new FileManagement(_logger.Object, 
                _pathConfigurationsMock.Object);

            base.Initialize();

            _pathConfigurationsMock.Setup(x => x.Value).Returns(PathConfigurations);
        }

        [Fact]
        public void ShouldReturnListOfFilesInDirectory()
        {                        
            var files = Directory.GetFiles(PathConfigurations.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

            var filesResult = _fileManagement.Scanner();

            filesResult.Should().Equal(files);
        }
        
        [Fact]
        public void ShouldSaveFileWithTheSummary()
        {

            var summaryResult = new SummaryResult
            {
                AmoutClients = 1,
                AmoutSalesman = 1,
                FileName = "Test.done.dat",
                IdExpensiveSale = "1a",
                WorstSalesman = "fulano"
            };

            _fileManagement.SaveFile(summaryResult);

            var files = Directory.GetFiles(PathConfigurations.RootPathOut, "Test.done.dat", SearchOption.TopDirectoryOnly).ToList();

            files.Any().Should().BeTrue();
            files.ForEach(File.Delete);
        }

        [Fact]
        public void ShouldEnsureThatOriginalFileMovedForProcessing()
        {
            var files = Directory.GetFiles(PathConfigurations.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

            files.ForEach(x =>
            {
                _fileManagement.MoveForProcessing(x);
            });

            var filesProcessing = Directory.GetFiles(PathConfigurations.RootPathProcessing, "*.dat", SearchOption.AllDirectories).ToList();

            filesProcessing.Any().Should().BeTrue();
        }
    }
}
