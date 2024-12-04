using System.ComponentModel.DataAnnotations;

namespace BurguerManiaAPI.Dto.UserOrder
{
    public class UserOrderResponse
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
    }
}