using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Drawing.Printing;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;
public class ProductsRepo : GenericRepo<Product>, IProductsRepo
{
    private readonly AbilityContext _context;

    public ProductsRepo(AbilityContext context) : base(context)
    {
        _context = context;
    }
    public float GetMaxPrice()
    {
        return _context.Products.Max(p=>p.Price);
    }
    public List<Product> GetOnSale() {
        return _context.Products
            .Include(p => p.Category)
            .Where(p=> p.Sale !=0)
            .ToList();
    }
    public int GetCount(int id, float min, float max)
    {
        if (id == 0)
            return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Price <= max && p.Price >= min)
            .Count();
        
        else
        {
            return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Price <= max && p.Price >= min && p.CategoryId == id)
            .Count();
        }

    }

    public int CountAll()
    {
            return _context.Products
            .Count();
    }
    public List<Product> GetAllWithPaging(int page, int id, float min, float max)
    {
        int pageSize = 9;
        int index = page - 1;
        if (id == 0)
        {
            return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Price <= max && p.Price >=min )
            .Skip(index * pageSize)
            .Take(pageSize)
            .ToList();
        }
        else
        {
            return _context.Products
            .Include(p => p.Category)
            .Where(p => p.Price <= max && p.Price >= min && p.CategoryId==id)
            .Skip(index * pageSize)
            .Take(pageSize)
            .ToList();
        }
    }
    public List<Product> GetAllWithCategory()
    {
        return _context.Products
            .Include(p => p.Category).ToList();

    }
    public Product? GetByIdWithCategory(int id)
    {
        return _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.ProductId == id);
    }
}
