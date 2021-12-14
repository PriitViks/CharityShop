using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
         : base(options)
        {
        }
        public DbSet<ShopItem> ShopItems => Set<ShopItem>();
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
            => await base.SaveChangesAsync(cancellationToken);
    }
}