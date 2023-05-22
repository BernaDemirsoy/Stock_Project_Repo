using StockProject.Entities.Entities;

namespace StockProject.UI.Areas.Admin.Models.DTOs
{
    public class OrderDetailDTO
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public Order Order { get; set; }
    }
}
