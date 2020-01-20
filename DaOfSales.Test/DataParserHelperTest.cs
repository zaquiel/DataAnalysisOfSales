using System;
using Xunit;
using FluentAssertions;
using DaOfSales.Domain;
using DaOfSales.Domain.Models;
using System.Collections.Generic;

namespace DaOfSales.Test
{
    public class DataParserHelperTest
    {
        private readonly IDataParserHelper _dataParserHelper;

        public DataParserHelperTest()
        {
            _dataParserHelper = new DataParserHelper();
        }

        [Fact]
        public void shouldBeAbleToProcessTheLineOfSalesman()
        {            
            var line = "001ç1234567891234çDiegoç50000";

            var salesmanExp = new Salesman
            {
                Cpf = "1234567891234",
                Name = "Diego",
                Salary = 50000
            };            

            var salesman = _dataParserHelper.Parser(line);

            salesman.Should().NotBeNull();
            salesman.Should().Be(salesmanExp);
        }      

        [Fact]
        public void shouldBeAbleToProcessTheLineOfCustomer()
        {
            var line = "002ç2345675434544345çJose da SilvaçRural";

            var customerExp = new Customer
            {
                Cnpj = "2345675434544345",
                Name = "Jose da Silva",
                BusinessArea = "Rural"
            };            

            var customer = _dataParserHelper.Parser(line);

            customer.Should().NotBeNull();
            customer.Should().Be(customerExp);
        }

        [Fact]
        public void shouldBeAbleToProcessTheLineOfSales()
        {
            var line = "003ç10ç[1-10-100,2-30-2.50,3-40-3.10]çDiego";

            var salesItemsExp = new List<SalesItem>
            {
                new SalesItem
                {
                    ItemId = "1",
                    Quantity = 10,
                    Price = 100
                },
                new SalesItem
                {
                    ItemId = "2",
                    Quantity = 30,
                    Price = 2.50
                },
                new SalesItem
                {
                    ItemId = "3",
                    Quantity = 40,
                    Price = 3.10
                }
            };

            var salesExp = new Sales
            {
                SalesId = "10",
                Items = salesItemsExp,
                SalesmanName = "Diego"
            };            

            var sales = _dataParserHelper.Parser(line);

            sales.Should().NotBeNull();
            sales.Should().Be(salesExp);
            sales.As<Sales>().Items.Should().BeEquivalentTo(salesItemsExp);                        
        }


        [Fact]
        public void shouldEnsureTypeIsValid()
        {
            var line = "000ç1234567891234çDiegoç50000";            

            var abstractEntity = _dataParserHelper.Parser(line);

            abstractEntity.Should().BeNull();
        }

    }
}
