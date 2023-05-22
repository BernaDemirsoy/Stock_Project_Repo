using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using StockProject.UI.Areas.Admin.Models.DTOs;
using System.Data;

namespace StockProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        string baseURL = "https://localhost:7270";

        public async Task<IActionResult> Index()
        {
            List<Order> orders = new List<Order>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetAllOrder"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    orders = JsonConvert.DeserializeObject<List<Order>>(apiResult);
                }

            }
            return View(orders);

        }
        public static Order _order;
        public static List<OrderDetail> orderDetails;

        public async Task<IActionResult> DetailOrder(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetOrderById/{id}"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _order = JsonConvert.DeserializeObject<List<Order>>(apiResult)[0];
                }
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetOrderDetails/{id}"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(apiResult);
                }

            }

            OrderDetailDTO orderDetailDTO = new OrderDetailDTO();
            orderDetailDTO.Order = _order;
            orderDetailDTO.OrderDetails = orderDetails;

            return View(orderDetailDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedOrder(int id)
        {
            using (var httpClient = new HttpClient())
            {
               
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/ConfirmedToOrder/{id}"))
                {
                   
                   
                }
               

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CancelledOrder(int id)
        {
            using (var httpClient = new HttpClient())
            {
                
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/CanceledToOrder/{id}"))
                {
                   
                }

            }
            return RedirectToAction("Index");
        }
    }
}
