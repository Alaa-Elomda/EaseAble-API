using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public class CartsRepo : GenericRepo<Cart>, ICartsRepo
{
    private readonly AbilityContext _context;
    public CartsRepo(AbilityContext context) : base(context)
    {
        _context = context;
    }
    public Cart? GetCartItem(Cart cart)
    {
        return _context.Carts.FirstOrDefault(c => c.UserId == cart.UserId && c.ProductId == cart.ProductId);
    }
    
    public List<Cart> GetCartByUserId(string userId)
    {
        return _context.Carts
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToList();

       
    }
    public void EmptyUserCart(string userId)
    {
        var toDelete = _context.Carts
            .Where(c => c.UserId == userId)
            .ToList();

        foreach (var item in toDelete)
        {
            Delete(item);
        }

    }
    public int? GetUserCartCount(string userId)
    {
        return _context.Carts
          .Where(c => c.UserId == userId)
          .Include(c => c.Product)
          .Count();

    }
}
