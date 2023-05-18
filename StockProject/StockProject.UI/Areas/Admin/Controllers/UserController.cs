using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using StockProject.Entities.Enums;
using System;
using System.Data;
using System.Text;

namespace StockProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        string baseURL = "https://localhost:7270";

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/User/GetAllUsers"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiResult);
                }

            }
            return View(users);
        }


        public IActionResult AddUser()
        {
            ViewBag.EnumValues = Enum.GetValues(typeof(UserRole));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            user.IsActive = true;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var answ = await httpClient.PostAsync($"{baseURL}/api/User/CreateUser", content))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();

                }
                
            }
            return RedirectToAction("Index");
        }
        static User updateduser;
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            ViewBag.EnumValues = Enum.GetValues(typeof(UserRole));
            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{baseURL}/api/User/GetUserById/{id}"))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                    updateduser = JsonConvert.DeserializeObject<User>(apiCevap);
                }
            }

            return View(updateduser);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User user)
        {

            using (var httpClient = new HttpClient())
            {
                user.IsActive = updateduser.IsActive;
                user.AddedDate = updateduser.AddedDate;

                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var cevap = await httpClient.PutAsync($"{baseURL}/api/User/UpdateUser/{user.Id}", content))
                {
                    string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteUser(int id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{baseURL}/api/User/DeleteUser/{id}"))
                {
                    //string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ActivetedUser(int id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.GetAsync($"{baseURL}/api/User/AvtivateUser/{id}"))
                {
                    //string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
