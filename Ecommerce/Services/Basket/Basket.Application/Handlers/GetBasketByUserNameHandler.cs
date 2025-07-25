using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.UserName);
        
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(basket);

            return shoppingCartResponse;
        }
    }
}
