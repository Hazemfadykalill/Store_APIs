using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Dtos.Baskets;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Repositories.Contract;

namespace Store.HazemFady.APIs.Controllers
{

    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        //get Basket By Id
        [HttpGet("GetBasket")]
        public async Task< ActionResult<UserBasket>> GetBasket(string? Id )
        {
            if (Id == null) return BadRequest(new APIErrorResponse(400,"Invalid Id !!" ));

            var basket =await basketRepository.GetBasketAsync( Id );
            if (basket == null) return new UserBasket() { Id=Id};

            return Ok(basket);


        }


        [HttpPost("CreateOrUpdateBasket")]
        public async Task<ActionResult<UserBasket>> CreateOrUpdateBasket(BasketDTO model)
        {

         var basket=   await basketRepository.UpdateBasketAsync(mapper.Map<UserBasket>(model));

            if (basket == null) return BadRequest(new APIErrorResponse(400));
            return Ok(basket);


        }


        [HttpDelete("DeleteBasket")]

        public async Task DeleteBasket(string Id)
        {


            await basketRepository.DeleteBasketAsync(Id);



        }

    }
}
