﻿using AbilitySystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.BL;

public class CategoriesManager : ICategoriesManager
{
    private readonly ICategoriesRepo _categoriesRepo;
    public CategoriesManager(ICategoriesRepo categoriessRepo)
    {
        _categoriesRepo = categoriessRepo;
    }

    public void Add(CategoryAddDto category)
    {
        var newCategory= new Category
        {
            CategoryName= category.CategoryName,
        };

        _categoriesRepo.Add(newCategory);
        _categoriesRepo.SaveChanges();
    }

    public void Delete(Category category)
    {
        _categoriesRepo.Delete(category);
        _categoriesRepo.SaveChanges();
    }

    public Category? Get(int id)
    {
        var category = _categoriesRepo.GetById(id);
        if (category == null)
        {
            return null;
        }
        return category;
    }

    public List<CategoryDto> GetAll()
    {
        var categories = _categoriesRepo.GetAll();
        return categories.Select(c => new CategoryDto(
            c.CategoryId,
            c.CategoryName)).ToList();
    }

    public CategoryWithProductDto? GetByIdWithProducts(int id)
    {
        Category? category = _categoriesRepo.GetByIdWithProducts(id);
        if (category == null) { return null; }
        return new CategoryWithProductDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Products = category.Products.Select(p => new ProductDto(
             p.ProductId,
             p.Name,
             p.Price,
             p.Sale,
             p.Description, p.Quantity,
             p.ImgURL,
             p.CategoryId
             )).ToList()
        };
    }
    public void Update(CategoryDto category)
    {
        var newCategory = _categoriesRepo.GetById(category.CategoryId);
        newCategory.CategoryId = category.CategoryId;
        newCategory.CategoryName = category.CategoryName;
        _categoriesRepo.SaveChanges();
    }

    public int CountAll()
    {
        return _categoriesRepo.CountAll();
    }

    public Dictionary<string, int> CountProductsInEachCategory()
    {
        return _categoriesRepo.CountProductsInEachCategory();
    }
}
