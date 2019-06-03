using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Domain.Models
{
    public class SummaryResult
    {
        public string FileName { get; set; }
        public int AmoutClients { get; set; }
        public int AmoutSalesman { get; set; }
        public string IdExpensiveSale { get; set; }
        public string WorstSalesman { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SummaryResult result &&
                   FileName == result.FileName &&
                   AmoutClients == result.AmoutClients &&
                   AmoutSalesman == result.AmoutSalesman &&
                   IdExpensiveSale == result.IdExpensiveSale &&
                   WorstSalesman == result.WorstSalesman;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FileName, AmoutClients, AmoutSalesman, IdExpensiveSale, WorstSalesman);
        }
    }
}
