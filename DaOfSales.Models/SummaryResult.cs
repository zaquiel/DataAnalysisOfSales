using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Models
{
    public class SummaryResult
    {
        public string FileName { get; set; }
        public int AmoutClients { get; set; }
        public int AmoutSalesman { get; set; }
        public string IdExpensiveSale { get; set; }
        public string WorstSalesman { get; set; }
    }
}
