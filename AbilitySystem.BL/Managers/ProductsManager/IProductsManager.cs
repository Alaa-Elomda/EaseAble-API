using AbilitySystem.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BL;
public interface IProductsManager
{
    List<ProductReadDto> GetAll();
    float GetMaxPrice();
    List<ProductReadDto> GetOnSale();
    int GetCount(int id, float min, float max);
    int CountAll();

    List<ProductReadDto> GetAllWithPaging(int page, int id, float min, float max);
    Product? Get(int id);
    ProductReadDto? GetByIdWithCategory(int id);
    void Add(ProductAddDto product);
    void Delete(Product product);
    void Update(ProductDto product);
    void UpdateImage(IFormFile? image, int id);
}

