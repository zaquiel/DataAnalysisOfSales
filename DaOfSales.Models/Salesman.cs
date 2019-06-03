using System;
using System.Collections.Generic;
using System.Text;

namespace DaOfSales.Models
{
    public class Salesman: AbstractEntity
    {        
        public string Cpf { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Salesman salesman &&
                   Cpf == salesman.Cpf &&
                   Name == salesman.Name &&
                   Salary == salesman.Salary;
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
