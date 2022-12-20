using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BackendWebshop.Context;
using BackendWebshop.Models;

namespace BackendWebshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext;
        public ItemController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult GetAllItems()
        {
            var items = dbContext.Items.ToList();
            return Ok(items);
        }

        [Route("/[controller]/{id}")]
        [HttpGet]
        public string GetItemByID(int ItemID)
        {
            var byId = from Item in dbContext.Items
                       where Item.ItemID == ItemID
                       select Item;
            return JsonConvert.SerializeObject(byId);
        }

/*        [Route("/[controller]/Create")]
        [HttpPost]
        public IActionResult SaveItem(Item items)
        {
            dbContext.Items.Add(items);
            dbContext.SaveChanges();
            return Ok();
        }*/
    }
}
