using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;

public class OrdersRepo : GenericRepo<Order>, IOrdersRepo
{
    public readonly AbilityContext _context;
    public OrdersRepo(AbilityContext context) : base(context)
    {
        _context = context;
    }

    public Order? GetByIdWithProducts(int id)
    {
        return _context.Orders
        .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
        .FirstOrDefault(o => o.OrderId == id);

    }

    public List<Order>? GetByUserWithProducts(string userId)
    {
        return _context.Orders
          .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
          .Where(o => o.UserId == userId)
        .ToList();
    }

    public int CountAll()
    {
        return _context.Orders
        .Count();
    }

    public int CountAccepted()
    {
        return _context.Orders
            .Where(o => o.OrderStatus == OrderStatus.Accepted)
            .Count();
    }
    public int CountRejected()
    {
        return _context.Orders
            .Where(o => o.OrderStatus == OrderStatus.Rejected)
            .Count();
    }

    public int CountPending()
    {
        return _context.Orders
    .Where(o => o.OrderStatus == OrderStatus.Pending)
    .Count();
    }
}
