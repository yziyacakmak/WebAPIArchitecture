﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category,int>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return context.Categories.Include(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<Category> GetCategoryWithProducts()
        {
            return context.Categories.Include(x => x.Products).AsQueryable();
        }
    }
}
