using DaOfSales.Domain.Models;
using System;
using System.Linq;

namespace DaOfSales.Domain
{
    public class DataParserHelper
    {
        public AbstractEntity Parser(string line)
        {
            AbstractEntity result = null;

            var lineArray = line.Split("ç");

            switch (lineArray[0])
            {
                case AbstractEntity.SALESMAN_TYPE_ID:
                    result = SalesmanParser(lineArray);
                    break;
                case AbstractEntity.CUSTOMER_TYPE_ID:
                    result = CustomerParser(lineArray);
                    break;
                case AbstractEntity.SALES_TYPE_ID:
                    result = SalesParser(lineArray);
                    break;
                default:
                    break;
            }            

            return result;
        }        

        private Salesman SalesmanParser(string[] lineArray)
        {
            return new Salesman
            {
                Cpf = lineArray[1],
                Name = lineArray[2],
                Salary = double.Parse(lineArray[3])
            };
        }

        private Customer CustomerParser(string[] lineArray)
        {
            return new Customer
            {
                Cnpj = lineArray[1],
                Name = lineArray[2],
                BusinessArea = lineArray[3]
            };
        }

        private Sales SalesParser(string[] lineArray)
        {
            return new Sales
            {
                SalesId = lineArray[1],
                Items = lineArray[2].Replace("[","").Replace("]", "").Split(",").Select(x => new SalesItem
                {
                    ItemId = x.Split("-")[0],
                    Quantity = int.Parse(x.Split("-")[1]),
                    Price = double.Parse(x.Split("-")[2].Replace(".", ","))
                }).ToList(),
                SalesmanName = lineArray[3]
            };
        }
    }
}
