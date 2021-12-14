using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShopItems.Queries
{
    public class RemoveItemFromBasketRequest : IRequest<ShopItemDto>
    {
        public ShopItemDto ShopItem { get; set; }
    }

    public class RemoveItemFromBasketRequestHandler : IRequestHandler<RemoveItemFromBasketRequest, ShopItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RemoveItemFromBasketRequestHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopItemDto> Handle(RemoveItemFromBasketRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.ShopItems
                .FirstAsync(e => e.Id == request.ShopItem.Id, cancellationToken: cancellationToken);

            item.Quantity += request.ShopItem.Quantity;
            _context.ShopItems.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ShopItemDto>(item);
        }
    }
}
