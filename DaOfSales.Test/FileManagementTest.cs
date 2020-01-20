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

        private PathConfigurations _pathConfigurations;

        public FileManagementTest()
        {
            _logger = new Mock<ILogger<FileManagement>>();
            _pathConfigurationsMock = new Mock<IOptions<PathConfigurations>>();

            _pathConfigurations = new PathConfigurations
            {
                RootPathIn = @".\DataIn\",
                RootPathOut = @".\DataOut\",
                RootPathProcessing = @".\DataProcessing\"
            };

            _pathConfigurationsMock.Setup(x => x.Value).Returns(_pathConfigurations);            

            _fileManagement = new FileManagement(_logger.Object, 
                _pathConfigurationsMock.Object);

            base.Initialize();
        }

        [Fact]
        public void ShouldReturnListOfFilesInDirectory()
        {                        
            var files = Directory.GetFiles(_pathConfigurations.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

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

            _fileManagement.SaveFile(summaryResult, configuration);

            var files = Directory.GetFiles(configuration.RootPathOut, "Test.done.dat", SearchOption.TopDirectoryOnly).ToList();

            files.Any().Should().BeTrue();
            files.ForEach(File.Delete);
        }

        [Fact]
        public void ShouldEnsureThatOriginalFileMovedForGarbage()
        {
            var configuration = new Configuration
            {
                RootPathIn = @".\DataIn\",
                RootPathGarbage = @".\DataGarbage\"
            };

            var files = Directory.GetFiles(configuration.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

            files.ForEach(x =>
            {
                _fileManagement.MoveForGarbage(x, configuration);
            });

            var filesGarbage = Directory.GetFiles(configuration.RootPathGarbage, "*.dat", SearchOption.AllDirectories).ToList();

            filesGarbage.Any().Should().BeTrue();
        }
    }
}
