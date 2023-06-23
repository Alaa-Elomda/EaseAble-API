using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;
public interface IProductsRepo: IGenericRepo<Product>
{

    float GetMaxPrice();
    List<Product> GetOnSale();

    List<Product> GetAllWithCategory();
    int GetCount(int id, float min, float max);
    int CountAll();
    List<Product> GetAllWithPaging(int page,int id,float min, float max);
    Product? GetByIdWithCategory(int id);
}
