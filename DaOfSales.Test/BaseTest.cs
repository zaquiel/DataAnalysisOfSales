using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DaOfSales.Domain;
using DaOfSales.Domain.Models;

namespace DaOfSales.Test
{
    public abstract class BaseTest
    {
        protected PathConfigurations PathConfigurations { get; set; }

        private readonly IDataParserHelper _dataParserHelper;

        public BaseTest()
        {
            _dataParserHelper = new DataParserHelper();
        }

        protected AbstractEntity ParserHelper(string line)
        {
            return _dataParserHelper.Parser(line);
        }

        protected void Initialize()
        {

            PathConfigurations = new PathConfigurations
            {
                RootPathIn = @"DataIn",
                RootPathOut = @"DataOut",
                RootPathProcessing = @"DataProcessing"
            };

            var files = Directory.GetFiles(@"BaseTestFiles/", "*.dat", SearchOption.AllDirectories).ToList();            

            files.ForEach(x =>
            {
                var destination = Path.Combine(PathConfigurations.RootPathIn, Path.GetFileName(x));
                if (!File.Exists(destination))
                {
                    File.Copy(x, destination);
                }
            });
        }        
    }
}
