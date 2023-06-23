using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Sale { get; set; }
    public string? Description { get; set; }

    public int Quantity { get; set; }

    public string? ImgURL { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }


    public ICollection<User> Users { get; set; } = new HashSet<User>(); 

    public ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();


    public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
}
