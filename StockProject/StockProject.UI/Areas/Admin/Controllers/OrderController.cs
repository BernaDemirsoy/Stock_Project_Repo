using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StockProject.Entities.Entities;
using StockProject.UI.Areas.Admin.Models.DTOs;
using System.Text;

namespace StockProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        string baseURL = "https://localhost:7270";

        public async Task< IActionResult> Index()
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

        
        public async Task<IActionResult> AddOrder()
        {
            List<OrderDetail> _orderdetails = new List<OrderDetail>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetAllActive"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _orderdetails = JsonConvert.DeserializeObject<List<OrderDetail>>(apiResult);
                }

            }
            List<User> _user = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Supplier/GetAllActive"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _user = JsonConvert.DeserializeObject<List<User>>(apiResult);
                }

            }
            AddOrderDTO oderDTO = new AddOrderDTO()
            {
                categories = _orderdetails,
                suppliers = _supplier
            };
            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderDTO productDto)
        {
            Order product = new Order()
            {
                OrderId = productDto.OrderId,
                SupplierId = productDto.SupplierId,
                UnitPrice = productDto.product.UnitPrice,
                Stock = productDto.product.Stock,
                ExpireDate = productDto.product.ExpireDate,
                OrderName = productDto.product.OrderName,
                IsActive = true,
            };
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");
                using (var answ = await httpClient.PostAsync($"{baseURL}/api/Order/CreateOrder",content))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    
                }

            }
            return RedirectToAction("Index");
        }
        static Order _productt;
        static List<Order> _orderdetailss;
        static List<StockProject.Entities.Entities.Supplier> _supplierr;

        public async Task<IActionResult> UpdateOrder(int id)
        {
          
          
            using (var httpClient = new HttpClient())
            {
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetOrderById/{id}"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _productt = JsonConvert.DeserializeObject<Order[]>(apiResult)[0];
                }
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Order/GetAllActive"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _orderdetailss = JsonConvert.DeserializeObject<List<Order>>(apiResult);
                }
                using (var answ = await httpClient.GetAsync($"{baseURL}/api/Supplier/GetAllActive"))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();
                    _supplierr = JsonConvert.DeserializeObject<List<StockProject.Entities.Entities.Supplier>>(apiResult);
                }

            }
            
           
            UpdateOrderDTO productDTO = new UpdateOrderDTO()
            {
                OrderId= _productt.Id,
                product= _productt,
                categories = _orderdetailss,
                suppliers = _supplierr
            };

            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO updateOrderDTO)
        {
            Order product = new Order()
            {
                OrderId = updateOrderDTO.OrderId,
                SupplierId = updateOrderDTO.SupplierId,
                UnitPrice = updateOrderDTO.product.UnitPrice,
                Stock = updateOrderDTO.product.Stock,
                ExpireDate = updateOrderDTO.product.ExpireDate,
                OrderName = updateOrderDTO.product.OrderName,
                IsActive = updateOrderDTO.product.IsActive,
            };
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                using (var answ = await httpClient.PostAsync($"{baseURL}/api/Order/UpdateOrder", content))
                {
                    string apiResult = await answ.Content.ReadAsStringAsync();

                }

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var cevap = await httpClient.DeleteAsync($"{baseURL}/api/Order/DeleteOrder/{id}"))
                {
                    //string apiCevap = await cevap.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }

    }
}
