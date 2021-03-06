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
    [Route("api/categories")]
    public class CategoriesController : Controller
    {

        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();

            return Ok(categories.Select(x => GetCategoryViewModel(x)));
        }

        // api/categories/id
        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = GetCategoryViewModel(category);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                Id = 123
                
            };

            _dataService.CreateCategory(category);

            return Created(GetUrl(category), GetCategoryViewModel(category));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryViewModel model)
        {
            var category = new Category
            {
                Id = id,
                Name = model.Name,
                Description = model.Description
            };

            if (!_dataService.UpdateCategory(category))
            {
                return NotFound();
            }

            return Ok(GetCategoryViewModel(category));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Console.WriteLine("delete invoked");
            if(!_dataService.DeleteCategory(id))
            {
                return NotFound();
            }
            return Ok();
        }





        private CategoryViewModel GetCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {

                Url = GetUrl(category),
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        private string GetUrl(Category category)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
        }


    }




}
