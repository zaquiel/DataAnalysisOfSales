using DaOfSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Domain
{
    public interface IFileManagement
    {
        List<string> Scanner(Configuration configuration);
        void SaveFile(SummaryResult summaryResult, Configuration configuration);

        void MoveForGarbage(string filePath, Configuration configuration);
    }
}
