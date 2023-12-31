﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilitySystem.DAL;
public class CategoriesRepo : GenericRepo<Category>, ICategoriesRepo
{
    private readonly AbilityContext _context;

    public CategoriesRepo(AbilityContext context) : base(context)
    {
        _context = context;
    }

    public Category? GetByIdWithProducts(int id)
    {
        return _context.Categories
            .Include(c => c.Products)
            .FirstOrDefault(c => c.CategoryId == id);
    }

    public int CountAll()
    {
        return _context.Categories
        .Count();
    }
    public Dictionary<string, int> CountProductsInEachCategory()
    {
        var categoryCounts = _context.Categories
            .Include(c => c.Products)
            .ToDictionary(c => c.CategoryName, c => c.Products.Count);

        return categoryCounts;
    }
}
