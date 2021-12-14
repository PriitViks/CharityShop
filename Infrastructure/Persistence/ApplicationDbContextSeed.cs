using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultDataAsync(ApplicationDbContext context)
        {
            if (!context.ShopItems.Any())
            {
                context.ShopItems.AddRange(
                    new ShopItem { ItemName = "Brownie", Price = 0.65M, Quantity = 48, Category = Domain.Enums.ItemCategory.Food, Picture = "Brownie.jpg" },
                    new ShopItem { ItemName = "Muffin", Price = 1, Quantity = 36, Category = Domain.Enums.ItemCategory.Food, Picture = "Muffin.jpg" },
                    new ShopItem { ItemName = "Cake Pop", Price = 1.35M, Quantity = 24, Category = Domain.Enums.ItemCategory.Food, Picture = "Cake Pop.jpg" },
                    new ShopItem { ItemName = "Apple tart", Price = 1.50M, Quantity = 60, Category = Domain.Enums.ItemCategory.Food, Picture = "Apple tart.jpg" },
                    new ShopItem { ItemName = "Water", Price = 1.50M, Quantity = 30, Category = Domain.Enums.ItemCategory.Food, Picture = "Water.jpg" },
                    new ShopItem { ItemName = "Shirt", Price = 2.00M, Quantity = 0, Category = Domain.Enums.ItemCategory.Clothing, Picture = "Shirt.jpg" },
                    new ShopItem { ItemName = "Pants", Price = 3.00M, Quantity = 0, Category = Domain.Enums.ItemCategory.Clothing, Picture = "Pants.jpg" },
                    new ShopItem { ItemName = "Jacket", Price = 4.00M, Quantity = 0, Category = Domain.Enums.ItemCategory.Clothing, Picture = "Jacket.jpg" },
                    new ShopItem { ItemName = "Toy", Price = 1.00M, Quantity = 0, Category = Domain.Enums.ItemCategory.Clothing, Picture = "Toy.jpg" }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
