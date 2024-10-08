using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.API.Dtos;
using Services.API.Services;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet(Name = "Basket")]
        public async Task<IActionResult> GetBasket(Guid buyerId)
        {
            var basket = await basketService.GetBasketsAsync(buyerId);

            if (basket == null)
            {
                return NotFound();
            }
            BasketDto basketDto = BasketDto.MapDTO(basket);

            return Ok(basketDto);
        }

        [HttpPost(Name = "Basket")]

        public async Task<IActionResult> AddItemToBascket(Guid productId, int quantity,Guid buyerId)
        {
            
            var result = await basketService.AddItemToBasketAsync(productId, quantity, buyerId);

            if (result)
            {
                //return CreatedAtRoute("Basket", BasketDto.MapDTO(basket));

                return Ok();

            }
            return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(Guid productId, int quantity , Guid buierId)
        {

            var result = await basketService.RemoveItemFromBasket(productId, quantity, buierId);

            if (result)
            {
                return Ok();
            }

            return BadRequest(new ProblemDetails { Title = "could not save" });

        }


    }
}
