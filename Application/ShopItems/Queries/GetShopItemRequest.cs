using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShopItems.Queries
{
    public class GetShopItemRequest : IRequest<IEnumerable<ShopItemDto>>
    {
    }

    public class GetShopItemsRequestHandler : IRequestHandler<GetShopItemRequest, IEnumerable<ShopItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetShopItemsRequestHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShopItemDto>> Handle(GetShopItemRequest request, CancellationToken cancellationToken)
        {
            return await _context.ShopItems
             .AsNoTracking()
             .ProjectTo<ShopItemDto>(_mapper.ConfigurationProvider)
             .OrderBy(e => e.ItemName)
             .ToListAsync(cancellationToken);
        }
    }
}
