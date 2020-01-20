using DaOfSales.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DaOfSales.Services
{    
    public class FileProcessingService: IFileProcessingService
    {        
        private IFileManagement _fileManagement;
        private IFileProcessor _fileProcessor;                        
        private ILogger<FileProcessingService> _logger;

        public FileProcessingService(ILogger<FileProcessingService> logger,
            IFileManagement fileManagement, 
            IFileProcessor fileProcessor)
        {            
            _fileManagement = fileManagement;
            _fileProcessor = fileProcessor;
            _logger = logger;
        }

        public void ProcessFiles()
        {
            try
            {
                foreach (var file in _fileManagement.Scanner())
                {
                    var fileProcessing = _fileManagement.MoveForProcessing(file);

                    var summaryResult = _fileProcessor.SummarizeFile(fileProcessing);

                    if (summaryResult != null)
                    {
                        _fileManagement.SaveFile(summaryResult);
                    }

                    //delete file                    
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Process file error!");
            }
        }
    }
}
