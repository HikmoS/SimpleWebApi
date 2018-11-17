using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Model;

namespace SimpleWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]

    public class ProductsController : Controller
    {
        public readonly MyContext context; //Proporties

        public ProductsController(MyContext _context)
        {
            context = _context;

            if (_context.Products.Count() == 0)
            {
                context.Products.Add(new Product { Id = 1, Name = "Popkek", UnitPrice = 10 });
                context.Products.Add(new Product { Id = 2, Name = "Topkek", UnitPrice = 20 });
                context.Products.Add(new Product { Id = 3, Name = "Today Kek", UnitPrice = 30 });
                context.Products.Add(new Product { Id = 4, Name = "Kalem", UnitPrice = 40 });

                context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return context.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]

        public IActionResult Get(int id)
        {
            var product = context.Products.FirstOrDefault(t => t.Id == id);
            
            if(product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product newProduct)
        {
            if(newProduct == null)
            {
                return BadRequest();
            }
            context.Products.Add(newProduct);
            context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct); //201 döndürür. ve CreatedAtRoute ile beraber KAyıt olunca id yi GetProduct ismindeki methoda yollar.
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Product upProduct,int id)
        {
            if(upProduct == null)
            {
                return BadRequest();
            }

            var product = context.Products.FirstOrDefault(t => t.Id == id);
            if (product == null) {
                return NotFound();
            }

            product.Name = upProduct.Name;
            product.UnitPrice = upProduct.UnitPrice;

            context.Products.Update(product);
            context.SaveChanges();
            return new NoContentResult();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.Products.FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            context.SaveChanges();
            return new NoContentResult();
        }

    }
}