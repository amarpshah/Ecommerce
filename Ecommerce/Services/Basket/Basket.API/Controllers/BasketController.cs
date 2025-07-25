using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        public IMediator Mediator { get; }

        public BasketController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task< ActionResult<ShoppingCartResponse>> GetBasketByUserName(string userName)
        { 
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await Mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand )
        {
            var basket = await Mediator.Send(createShoppingCartCommand);        
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasketByUserName(string userName)
        { 
            var cmd = new DeleteBasketByUserNameCommand(userName);
            await Mediator.Send(cmd);
            return Ok();
        }
    }
}
