using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using System.Text;

namespace StockProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        string baseURL = "https://localhost:7270";

        public async Task< IActionResult> Index()
        {
            List<Category> categories = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Category/GetAllCategories"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(apiResult);
                }

            }
            return View(categories);
        }

        
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            category.IsActive = true;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(category),Encoding.UTF8,"application/json");
                using (var answ = await httpClient.PostAsync($"{baseURL}/api/Category/CreateCategory",content))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    
                }

            }
            return RedirectToAction("Index");
        }
    }
}
