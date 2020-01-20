using DaOfSales.Domain.Models;

namespace DaOfSales.Domain
{
    public interface IDataParserHelper
    {
        AbstractEntity Parser(string line);
    }
}