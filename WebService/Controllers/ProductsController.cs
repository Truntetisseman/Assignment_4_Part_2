﻿using DataServiceLib;
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
    [Route("api/products")]
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




        private ProductViewModel GetProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Url = GetUrl(product),
                Name = product.Name,
                Category = product.Category
            };
        }


        private string GetUrl(Product product)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetProducts), new { product.Id });
        }
    }

}


