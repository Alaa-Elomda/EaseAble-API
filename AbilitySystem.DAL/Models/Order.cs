using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public class Order
{
    public int OrderId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public DateTime OrderDate { get; set; }

    public float TotalPrice { get; set;}

    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User? User { get; set; } 

    public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();

}

public enum OrderStatus
{
    Accepted, 
    Pending,
    Rejected
}
