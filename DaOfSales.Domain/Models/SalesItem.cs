namespace DaOfSales.Domain.Models
{
    public class SalesItem
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SalesItem item &&
                   ItemId == item.ItemId &&
                   Quantity == item.Quantity &&
                   Price == item.Price;
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
