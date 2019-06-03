using DaOfSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Domain
{
    public interface IFileProcessor
    {
        SummaryResult SummarizeFile(string filePath);
    }
}
