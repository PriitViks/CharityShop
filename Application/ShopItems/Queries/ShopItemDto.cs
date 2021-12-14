using Application.Common;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.ShopItems.Queries
{
    public class ShopItemDto : IMapFrom<ShopItem>
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Picture { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShopItem, ShopItemDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => MapItemCategory(s.Category)));
        }
        public static string MapItemCategory(ItemCategory category) => Enum.GetName(typeof(ItemCategory), category);
    }
}
