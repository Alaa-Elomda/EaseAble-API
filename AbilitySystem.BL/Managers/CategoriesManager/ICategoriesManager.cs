﻿using AbilitySystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BL;

public interface ICategoriesManager
{
    List<CategoryDto> GetAll();
    Category? Get(int id);
    CategoryWithProductDto? GetByIdWithProducts(int id);
    void Add(CategoryAddDto category);
    void Delete(Category category);
    void Update(CategoryDto category);
    int CountAll();
    Dictionary<string, int> CountProductsInEachCategory();
}
