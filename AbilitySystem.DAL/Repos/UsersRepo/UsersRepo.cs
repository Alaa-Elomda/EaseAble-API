using Microsoft.EntityFrameworkCore;

namespace AbilitySystem.DAL;

public class UsersRepo : GenericRepo<User>, IUsersRepo
{
    public readonly AbilityContext _context;
    public UsersRepo(AbilityContext context) : base(context)
    {
        _context = context;
    }

    public void AddToWishlist(string userId, int productId)
    {
        var product= _context.Set<Product>().FirstOrDefault(p => p.ProductId == productId);
        if(product == null) { return; } 
        _context.AppUsers
            .Include(u => u.Products)
            .FirstOrDefault(u => u.Id == userId)?
            .Products.Add(product);
    }

    public void DeleteFromWishlist(string userId, int productId)
    {
        var product = _context.Set<Product>().FirstOrDefault(p => p.ProductId == productId);
        if (product == null) { return; }
        _context.AppUsers
            .Include(u => u.Products)
            .FirstOrDefault(u => u.Id == userId)?
            .Products.Remove(product);
    }

    public User? Get(string id)
    {
        return _context.Set<User>().Find(id);
    }
    public int? GetUserWishlistCount(string id)
    {
        return _context.AppUsers
           .Include(u => u.Products)
           .FirstOrDefault(u => u.Id == id)?
           .Products.Count();
    }
    public List<Product>? GetUserWithWishlist(string id)
    {

        return _context.AppUsers
            .Include(u => u.Products)
            .FirstOrDefault(u=>u.Id==id)?
            .Products.ToList();
    }

    public int CountAll()
    {
        return _context.AppUsers
        .Count();
    }

    public int CountMales()
    {
        return _context.AppUsers
            .Where(m=>m.Gender == Gender.Male)
        .Count();
    }

    public int CountFemales()
    {
        return _context.AppUsers
            .Where(m => m.Gender == Gender.Female)
        .Count();
    }

}
