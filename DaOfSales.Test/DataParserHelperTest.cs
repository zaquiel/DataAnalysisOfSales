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
        [Fact]
        public void shouldBeAbleToProcessTheLineOfSalesman()
        {            
            var line = "001Á1234567891234ÁDiegoÁ50000";

            var salesmanExp = new Salesman
            {
                Cpf = "1234567891234",
                Name = "Diego",
                Salary = 50000
            };

            DataParserHelper dataParserHelper = new DataParserHelper();

            var salesman = dataParserHelper.Parser(line);

            salesman.Should().NotBeNull();
            salesman.Should().Be(salesmanExp);
        }      

        [Fact]
        public void shouldBeAbleToProcessTheLineOfCustomer()
        {
            var line = "002Á2345675434544345ÁJose da SilvaÁRural";

            var customerExp = new Customer
            {
                Cnpj = "2345675434544345",
                Name = "Jose da Silva",
                BusinessArea = "Rural"
            };

            DataParserHelper dataParserHelper = new DataParserHelper();

            var customer = dataParserHelper.Parser(line);

            customer.Should().NotBeNull();
            customer.Should().Be(customerExp);
        }

        [Fact]
        public void shouldBeAbleToProcessTheLineOfSales()
        {
            var line = "003Á10Á[1-10-100,2-30-2.50,3-40-3.10]ÁDiego";

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

            DataParserHelper dataParserHelper = new DataParserHelper();

            var sales = dataParserHelper.Parser(line);

            sales.Should().NotBeNull();
            sales.Should().Be(salesExp);
            sales.As<Sales>().Items.Should().BeEquivalentTo(salesItemsExp);                        
        }


        [Fact]
        public void shouldEnsureTypeIsValid()
        {
            var line = "000Á1234567891234ÁDiegoÁ50000";

            DataParserHelper dataParserHelper = new DataParserHelper();

            var abstractEntity = dataParserHelper.Parser(line);

            abstractEntity.Should().BeNull();
        }

    }
}
