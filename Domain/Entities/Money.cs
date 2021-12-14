namespace Domain.Entities
{
    public class Money
    {
        public Money(decimal name, int quantiy)
        {
            Name = name < 1 ? decimal.ToInt32(name * 100) + " Cents" : name + " Euros";
            Quantity = quantiy;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
