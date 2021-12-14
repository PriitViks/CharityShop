using Domain.Entities;
using MediatR;

namespace Application.ShopItems.Queries
{
    public class MoneyToReturnRequest : IRequest<IEnumerable<Money>>
    {
        public decimal MoneyPaid { get; set; }
    }

    public class MoneyToReturnRequestHandler : IRequestHandler<MoneyToReturnRequest, IEnumerable<Money>>
    {
        static readonly decimal[] money = { 0.01M, 0.02M, 0.05M, 0.10M, 0.20M, 0.50M, 1, 2, 5, 10, 20, 50, 100, 200, 500 };
        public Task<IEnumerable<Money>> Handle(MoneyToReturnRequest request, CancellationToken cancellationToken)
        {

            List<decimal> allBillsToReturn = new();

            var n = money.Length;

            for (int i = n - 1; i >= 0; i--)
            {
                while (request.MoneyPaid >= money[i])
                {
                    request.MoneyPaid -= money[i];
                    allBillsToReturn.Add(money[i]);
                }
            }
            var moneyToReturn = allBillsToReturn.GroupBy(s => s)
                  .Select(g => new Money(g.Key, g.Count()));

            return Task.FromResult(moneyToReturn);
        }
    }
}
