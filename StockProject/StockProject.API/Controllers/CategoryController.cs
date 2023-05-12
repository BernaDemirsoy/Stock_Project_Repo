using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockProject.Entities.Entities;
using StockProject.Service.Abstract;

namespace StockProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> service;

        public CategoryController(IGenericService<Category> service)
        {
            this.service = service;
        }
        //GET: api/Category/GetAllCategories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategorybyId(int id)
        {
            var category=service.GetById(id);
            if(category!=null)
            return Ok(category);
            else
                return NotFound("Böyle bir kategori bulunamadı");
        }
        
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            var result = service.Add(category);
            if (result)
            {
                var addedcategory = service.GetById(category.Id);
                return Ok(addedcategory);
            }
                
            else
                return NotFound("Kategori eklenemedi");
        }
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            var updatedCategory = service.GetById(category.Id);
            if (updatedCategory != null)
            {
                return Ok(service.Update(updatedCategory));
            }
            else
                return NotFound("Kategori güncellenemedi");
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var updatedCategory = service.GetById(id);
            if (updatedCategory != null)
            {
                return Ok(service.Remove(updatedCategory));
            }
            else
                return NotFound("Kategori silinemedi");
        }

    }
}
