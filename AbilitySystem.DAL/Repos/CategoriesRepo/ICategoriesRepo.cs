﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;
public interface ICategoriesRepo : IGenericRepo<Category>
{
    Category? GetByIdWithProducts(int id);
    int CountAll();
    Dictionary<string, int> CountProductsInEachCategory();

}
