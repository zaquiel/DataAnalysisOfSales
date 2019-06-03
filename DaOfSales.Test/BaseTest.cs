using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaOfSales.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            
        }

        public void Initialize()
        {
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
