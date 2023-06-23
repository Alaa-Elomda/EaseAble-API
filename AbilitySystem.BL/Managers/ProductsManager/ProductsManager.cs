using AbilitySystem.DAL;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BL;
public class ProductsManager : IProductsManager
{
    private readonly IProductsRepo _productsRepo;
    public ProductsManager(IProductsRepo productsRepo)
    {
        _productsRepo = productsRepo;
    }
    public void Add(ProductAddDto product)
    {
        string imgURL = UploadImageOnCloudinary(product.Image);
        var newProduct = new Product
        {
            Name= product.Name,
            Price= product.Price,
            Sale= product.Sale,
            Description= product.Description,
            Quantity= product.Quantity,
            ImgURL= imgURL,
            CategoryId= product.CategoryId
        };

        _productsRepo.Add(newProduct);
        _productsRepo.SaveChanges();
    }

    public void Delete(Product product)
    {
        _productsRepo.Delete(product);
        _productsRepo.SaveChanges();
    }

    public Product? Get(int id)
    {
        var product = _productsRepo.GetById(id);
        if (product == null)
        {
            return null;
        }
        return product;
    }
   
    public List<ProductReadDto> GetOnSale()
    {
        var products = _productsRepo.GetOnSale();
        return products.Select(p => new ProductReadDto(
            p.ProductId,
            p.Name,
            p.Price,
            p.Sale,
            p.Description,
            p.Quantity,
            p.ImgURL,
            p.Category?.CategoryName ?? ""
             )).ToList();

    }
    public float GetMaxPrice()
    {
        return _productsRepo.GetMaxPrice();
    }
    public int GetCount(int id, float min, float max)
    {
        return _productsRepo.GetCount(id, min, max);
    }

    public int CountAll()
    {
        return _productsRepo.CountAll();
    }
    public List<ProductReadDto> GetAllWithPaging(int page, int id, float min, float max)
    {
        var products = _productsRepo.GetAllWithPaging(page, id, min, max);
        return products.Select(p => new ProductReadDto(
            p.ProductId,
            p.Name,
            p.Price,
            p.Sale,
            p.Description,
            p.Quantity,
            p.ImgURL,
            p.Category?.CategoryName ?? ""
             )).ToList();

    }
    public List<ProductReadDto> GetAll()
    {

        var products = _productsRepo.GetAllWithCategory();
        return products.Select(p => new ProductReadDto(
            p.ProductId,
            p.Name,
            p.Price,
            p.Sale,
            p.Description,
            p.Quantity,
            p.ImgURL,
            p.Category?.CategoryName ?? ""
             )).ToList();
    }
    public ProductReadDto? GetByIdWithCategory(int id)
    {
        Product? product = _productsRepo.GetByIdWithCategory(id);
        if (product == null) { return null; }
        return new ProductReadDto(
            product.ProductId,
            product.Name,
            product.Price,
            product.Sale,
            product.Description,
            product.Quantity,
            product.ImgURL,
            product.Category?.CategoryName??""
             );
    }

    public void Update(ProductDto product)
    {
        var newProduct = _productsRepo.GetById(product.ProductId);
        newProduct!.ProductId= product.ProductId;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.Sale = product.Sale;
        newProduct.Description = product.Description;
        newProduct.Quantity = product.Quantity;
        newProduct.ImgURL = product.ImgURL;
        newProduct.CategoryId = product.CategoryId;
        _productsRepo.SaveChanges();
    }

    public void UpdateImage(IFormFile? image, int id)
    {

        string imgURL = UploadImageOnCloudinary(image);

        Product? productToUpdate = _productsRepo.GetById(id);

        if (productToUpdate is null)
        {
            return;
        }
        productToUpdate.ImgURL = imgURL;
        _productsRepo.Update(productToUpdate);
        _productsRepo.SaveChanges();
    }

    public static string UploadImageOnCloudinary(IFormFile? image)
    {
        if (image is not null)
        {

            var sentExtension = Path.GetExtension(image!.FileName).ToLower();

            string imageName = Guid.NewGuid() + sentExtension;

            var imagesFolderPath = "/Images";

            string imgURL = @$"{imagesFolderPath}/{imageName}";

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                image.CopyTo(stream);
            }

            Account account = new Account("dalk6iico", "354216316393478", "2t1XZOb7vimbi0D9hAgb5u8pbpw");

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fullPath)
            };
            var uploadResult = cloudinary.Upload(uploadParams);


            return uploadResult.Url.ToString();
        }
        else { return string.Empty; }


    }
}
