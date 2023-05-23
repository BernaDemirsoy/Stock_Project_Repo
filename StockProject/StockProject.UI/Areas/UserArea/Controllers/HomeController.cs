using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using System.Data;

namespace StockProject.UI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        string baseURL = "https://localhost:7270";

        public static List<Product> basket = new List<Product>();
        
        public async Task<IActionResult> Index()
        {
            
            List<Product> products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Product/GetAllActiveProduct"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResult);
                }

            }
            return View(products);
        }
       
        [HttpGet]
        public int SepettekiUrunSayisi()
        {

            return basket.Count;
        }
        static Product _productt;
        [HttpPost]
        public async void UrunleriListeyeEkle(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Product/GetProductById/{id}"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _productt = JsonConvert.DeserializeObject<Product[]>(apiResult)[0];
                }
                
            }
            basket.Add(_productt);
        }
    }
}
