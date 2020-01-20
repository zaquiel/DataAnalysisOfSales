using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DaOfSales.Domain.Models;

namespace DaOfSales.Test
{
    public abstract class BaseTest
    {
        public PathConfigurations PathConfigurations { get; set; }

        public BaseTest()
        {
            
        }

        public void Initialize()
        {

            PathConfigurations = new PathConfigurations
            {
                RootPathIn = @".\DataIn\",
                RootPathOut = @".\DataOut\",
                RootPathProcessing = @".\DataProcessing\"
            };

            var files = Directory.GetFiles(@".\BaseTestFiles\", "*.dat", SearchOption.AllDirectories).ToList();            

            files.ForEach(x =>
            {
                var destination = $@".\DataIn\{Path.GetFileName(x)}";
                if (!File.Exists(destination))
                {
                    File.Copy(x, destination);
                }
            });
        }
    }
}
