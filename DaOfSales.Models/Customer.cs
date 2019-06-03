using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Models
{    
    public class Customer: AbstractEntity
    {        
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string BusinessArea { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Customer customer &&
                   Cnpj == customer.Cnpj &&
                   Name == customer.Name &&
                   BusinessArea == customer.BusinessArea;
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
