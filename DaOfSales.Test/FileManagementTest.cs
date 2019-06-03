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
    public class FileManagementTest: BaseTest
    {
        private readonly IFileManagement _fileManagement;

        public FileManagementTest()
        {
            _fileManagement = new FileManagement();

            base.Initialize();
        }

        [Fact]
        public void ShouldReturnListOfFilesInDirectory()
        {
            var configuration = new Configuration
            {
                RootPathIn = @".\DataIn\"
            };

            var files = Directory.GetFiles(configuration.RootPathIn, "*.dat", SearchOption.AllDirectories).ToList();

            var filesResult = _fileManagement.Scanner(configuration);

            filesResult.Should().Equal(files);
        }
        
        [Fact]
        public void ShouldSaveFileWithTheSummary()
        {
            var configuration = new Configuration
            {
                RootPathOut = @".\DataOut\"
            };

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
