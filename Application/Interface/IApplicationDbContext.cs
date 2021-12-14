using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ShopItem> ShopItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
