using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Domain.Models
{
    public class Sales: AbstractEntity
    {
        public string SalesId { get; set; }
        public List<SalesItem> Items { get; set; }
        public string SalesmanName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Sales sales &&
                   SalesId == sales.SalesId &&                   
                   SalesmanName == sales.SalesmanName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
