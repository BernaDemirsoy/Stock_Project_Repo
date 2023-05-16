using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using StockProject.UI.Models;
using StockProject.UI.Models.DTOs;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace StockProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string baseURL = "https://localhost:7270"; //Web API!nın çoluştuğu sunucu portu ile birlikte olacak

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public  IActionResult  Index()
        {
           
            return View();
        }
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        //https://localhost:7270/api/User/Login?email=ba%40ba.com&password=12345.A
        public async Task<IActionResult> LogIn(LoginDTO loginVM)
        {
            User logged = new User();
            using (var httpClient=new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/User/Login?email={loginVM.Email}&password={loginVM.Password}"))
                {
                    string apiResult= await answ.Content.ReadAsStringAsync();
                    logged=JsonConvert.DeserializeObject<User>(apiResult);
                }

            }
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}