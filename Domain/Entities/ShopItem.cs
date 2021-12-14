using Domain.Enums;

namespace Domain.Entities
{
    public class ShopItem
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public ItemCategory Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Picture { get; set; }
    }
}
