using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_WebApi.Dto;
using Shop_WebApi.Models;

namespace Shop_WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class OrderController : Controller
    {
        ShopDbContext context = new ShopDbContext();

        public async Task<IActionResult> GetOrders([FromQuery] string userId, [FromQuery] string status)
        {
            try
            {
                var orders = context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems).ThenInclude(i => i.Product)
                    .ToList();

                if (!string.IsNullOrEmpty(userId))
                {
                    int userIdInt = Convert.ToInt32(userId);

                    orders = orders.Where(o => o.UserId == userIdInt).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(o => o.Status == status).ToList();
                }
                var query = orders.Select(o => new OrderResponseDto()

                {
                        OrderId = o.Id,
                        Customer = $"{o.User.LastName} {o.User.FirstName} {o.User.MiddleName}",
                        Status = GetStatus(o.Status),
                        CreatedAt = DateTime.Parse(o.CreatedAt).ToString("dd.MM.yyyy"),
                        Address = GetAddress(o.User),
                        itemsCount = o.OrderItems.Count,
                        Total = o.OrderItems.Sum(i => i.Quantity * i.Product.Price).ToString("F2"),
                        Items = o.OrderItems.Select(i => new ItemsResponseDto()
                        {
                            Title = i.Product.Title,
                            Quantity = i.Quantity,
                            Price = i.Product.Price.ToString("F2")

                        }).ToList()
                    }).ToList();

                return Ok(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto { Error = "Внутренняя ошибка приложения" });
            }
        }

        private string GetAddress(User user)
        {
            string address = $"г.{user.City} ул.{user.Street} д.{user.House}";

            if (string.IsNullOrWhiteSpace(user.Flat))
            {
                address += $",кв. {user.Flat}";
            }

            return address ;
        }

        private string GetStatus(string status) => status switch
        {
            "new" => "Новый",
            "paid" => "Оплачен",
            "shipped" => "Отправлен",
            "done" => "Завершен",
            "cancelled" => "Отменен",
            _ => status
            
        };
    }
}
