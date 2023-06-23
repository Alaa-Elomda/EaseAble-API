using AbilitySystem.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics.Metrics;


namespace AbilitySystem.BL;

public class UsersManager : IUsersManager
{
    private readonly IUsersRepo _usersRepo;

    private readonly IWebHostEnvironment _webHostEnvironment;


    public UsersManager(IUsersRepo usersRepo, IWebHostEnvironment webHostEnvironment)
    {
        _usersRepo = usersRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    public List<GetUserDto> GetAll()
    {
        List<User> usersFromDb = _usersRepo.GetAll();

        return usersFromDb
            .Select(u => new GetUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                Gender = u.Gender,
                ImgURL = u.ImgURL,

            })
            .ToList();
    }
    public GetUserDto Get(string id)
    {
        User? userFromDb = _usersRepo.Get(id);

        if (userFromDb != null)
        {

            return new GetUserDto
            {
                Id = userFromDb.Id,
                UserName = userFromDb.UserName,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Address = userFromDb.Address,
                Gender = userFromDb.Gender,
                ImgURL = userFromDb.ImgURL,

            };
        }
        return null;
    }
    public void Update(UpdateUserDto user, string id)
    {
        User? userToUpdate = _usersRepo.Get(id);

        if (userToUpdate is null)
        {
            return;
        }
        userToUpdate.UserName = user.UserName==null? userToUpdate.UserName : user.UserName;
        userToUpdate.Email = user.Email == null ? userToUpdate.Email: user.Email;
        userToUpdate.PhoneNumber = user.PhoneNumber == null ? userToUpdate.PhoneNumber : user.PhoneNumber;
        userToUpdate.Address = user.Address == null ? userToUpdate.Address: user.Address;



        _usersRepo.Update(userToUpdate);
        _usersRepo.SaveChanges();
    }
    public void Delete(string id)
    {
        User? userToDelete = _usersRepo.Get(id);
        if (userToDelete != null)
        {
            _usersRepo.Delete(userToDelete);
            _usersRepo.SaveChanges();
        }
    }

   
    public void UpdateImage(IFormFile? image, string id )
    {
        string imgURL = ProductsManager.UploadImageOnCloudinary(image);
       
        User? userToUpdate = _usersRepo.Get(id);

        if (userToUpdate is null)
        {
            return;
        }
        userToUpdate.ImgURL = imgURL;
        _usersRepo.Update(userToUpdate);
        _usersRepo.SaveChanges();
    }

    public List<GetUserWithWishlistDto>? GetUserWithWishlist(string id)
    {
        var wishlistItems = _usersRepo.GetUserWithWishlist(id);
        return wishlistItems?.Select(p => new GetUserWithWishlistDto
        {
            ProductId = p.ProductId,
            ProductName = p.Name,
            Price = p.Price,
            Sale = p.Sale,
            ImgURL = p.ImgURL
        }).ToList();
    }

    public void AddToWishlist(AddToWishlistDto wishlist)
    {
        
        _usersRepo.AddToWishlist(wishlist.userId, wishlist.productId);
        _usersRepo.SaveChanges();
    }

    public void DeleteFromWishlist(AddToWishlistDto wishlist)
    {
        _usersRepo.DeleteFromWishlist(wishlist.userId, wishlist.productId);
        _usersRepo.SaveChanges();
    }
    public int? GetUserWishlistCount(string id)
    {
        return _usersRepo.GetUserWishlistCount(id);
    }
    public int CountAll()
    {
        return _usersRepo.CountAll();
    }

    public int CountMales()
    {
        return _usersRepo.CountMales();
    }

    public int CountFemales()
    {
        return _usersRepo.CountFemales();
    }
}

