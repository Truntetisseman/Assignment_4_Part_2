using DataServiceLib;
using EfEx.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.ViewModels;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _dataService.GetProducts();

            return Ok(products.Select(x => GetProductViewModel((x))));
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = GetProductViewModel2(product);

            return Ok(model);
        }


        [HttpGet("category/{id}")]
        public IActionResult GetProductByCatId(int id)
        {
            var product = _dataService.GetProductsByCatId(id);

            if (product == null || product.Count == 0)
            {
                return NotFound(new List<Product>());
            }

            var model = product.Select(x => GetProductViewModel2(x));

            return Ok(model);
        }        
        [HttpGet("name/{needle}")]
        public IActionResult GetProductBySubstring(string needle)
        {
            var product = _dataService.GetProductByName(needle);

            if (product == null || product.Count == 0)
            {
                return NotFound(new List<Product>());
            }

            var model = product.Select(x => GetProductViewModel(x));

            return Ok(model);
        }





        private ProductViewModel GetProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Url = GetUrl(product),
                Id = product.Id,
                ProductName = product.Name,
                CategoryName = product.Category.Name,
                Category = product.Category,
                UnitPrice = product.UnitPrice,
                SupplierId = product.SupplierId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock
            };
        }private ProductViewModel GetProductViewModel2 (Product product)
        {
            return new ProductViewModel
            {
                Url = GetUrl(product),
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Category = product.Category,
                UnitPrice = product.UnitPrice,
                SupplierId = product.SupplierId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitsInStock = product.UnitsInStock
            };
        }


        private string GetUrl(Product product)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
        }
    }

}



