using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DaOfSales.Domain.Models
{
    public class Configuration
    {
        private readonly string _homePath = Environment.GetEnvironmentVariable("Homepath")+Path.DirectorySeparatorChar;        

        private string _rootPathIn;
        public string RootPathIn
        {
            get
            {
                if (string.IsNullOrEmpty(_rootPathIn))
                {
                    _rootPathIn = $"{_homePath}data{Path.DirectorySeparatorChar}in{Path.DirectorySeparatorChar}";
                }

                return _rootPathIn;
            }
            set
            {
                _rootPathIn = value;
            }
        }

        private string _rootPathOut;
        public string RootPathOut
        {
            get
            {
                if (string.IsNullOrEmpty(_rootPathOut))
                {
                    _rootPathOut = $"{_homePath}data{Path.DirectorySeparatorChar}out{Path.DirectorySeparatorChar}";
                }

                return _rootPathOut;
            }
            set
            {
                _rootPathOut = value;
            }
        }

        private string _rootPathGarbage;
        public string RootPathGarbage
        {
            get
            {
                if (string.IsNullOrEmpty(_rootPathGarbage))
                {
                    _rootPathGarbage = $"{_homePath}data{Path.DirectorySeparatorChar}Garbage{Path.DirectorySeparatorChar}";
                }

                return _rootPathGarbage;
            }
            set
            {
                _rootPathGarbage = value;
            }
        }
    }
}
