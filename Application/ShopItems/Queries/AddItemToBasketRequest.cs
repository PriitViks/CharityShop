using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShopItems.Queries
{
    public class AddItemToBasketRequest : IRequest<ShopItemDto>
    {
        public int Id { get; set; }
    }

    public class AddItemToBasketRequestHandler : IRequestHandler<AddItemToBasketRequest, ShopItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddItemToBasketRequestHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopItemDto> Handle(AddItemToBasketRequest request, CancellationToken cancellationToken)
        {
            var item = await _context.ShopItems
                .FirstAsync(e => e.Id == request.Id, cancellationToken: cancellationToken);

            if (item.Quantity > 0)
            {
                item.Quantity -= 1;
                _context.ShopItems.Update(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return null;
            }
            return _mapper.Map<ShopItemDto>(item);
        }
    }
}
