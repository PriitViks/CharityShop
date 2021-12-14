using Application.ShopItems.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CharityShop.Controllers
{
    public class ShopItemController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopItemDto>>> Get()
        {
            return Ok(await Mediator.Send(new GetShopItemRequest()));
        }

        [HttpPut]
        [Route("ItemSold")]
        public async Task<ActionResult<ShopItemDto>> ItemSold([FromBody] int soldId)
        {
            var updatedShopItem = await Mediator.Send(new AddItemToBasketRequest { Id = soldId });
            if (updatedShopItem == null) return Content("Sold out", "text/html");
            return Ok(updatedShopItem);
        }

        [HttpPut]
        [Route("ItemReturned")]
        public async Task<ActionResult<ShopItemDto>> ItemReturned([FromBody] ShopItemDto shopItem)
        {
            return Ok(await Mediator.Send(new RemoveItemFromBasketRequest { ShopItem = shopItem }));
        }

        [HttpPut]
        [Route("MoneyToReturn")]
        public async Task<ActionResult<IEnumerable<Money>>> MoneyToReturn([FromBody] decimal moneyPaid)
        {
            return Ok(await Mediator.Send(new MoneyToReturnRequest { MoneyPaid = moneyPaid }));
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile([FromForm(Name = "file")] IFormFile resultFile)
        {
            if (resultFile.Length == 0)
                return BadRequest("Empty file");
            else
            {
                var updateConfiguration = false;
                using StreamReader reader = new(resultFile.OpenReadStream());
                var itemsToUpdate = JsonConvert.DeserializeObject<IList<ShopItem>>(reader.ReadToEnd());

                if (itemsToUpdate.Any())
                {
                    updateConfiguration = await Mediator.Send(new LoadShopItemFromFileRequest { ShopItems = itemsToUpdate });
                }
                return updateConfiguration ? Ok("Successfully update database") : BadRequest("Failed to update database");
            }
        }
    }
}