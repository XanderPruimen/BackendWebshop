using VetsendBackend.Data;
using VetsendBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetsendBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly DataContext db;
        public ProductController(DataContext db)
        {
            this.db = db;
        }


        [HttpGet]

          public IActionResult GetAllProducts()
        {
            var products = db.Products.ToList();
            return Ok(products);
        }

        [Route("/[controller]/{id}")]
        [HttpGet]
        public string GetProductByID(int id)
        {
            var byId = from Product in db.Products
                        where Product.productID == id
                        select Product;
            return JsonConvert.SerializeObject(byId);
        }
        [Route("/[controller]/Create")]
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return Ok();
        }
    }
}
