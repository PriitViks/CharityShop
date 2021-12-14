using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ShopItems.Queries
{
    public class LoadShopItemFromFileRequest : IRequest<bool>
    {
        public IList<ShopItem> ShopItems { get; set; }
    }

    public class LoadConfigurationFromFileHandler : IRequestHandler<LoadShopItemFromFileRequest, bool>
    {
        private readonly IApplicationDbContext _context;

        public LoadConfigurationFromFileHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(LoadShopItemFromFileRequest request, CancellationToken cancellationToken)
        {
            _context.ShopItems.UpdateRange(request.ShopItems);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
