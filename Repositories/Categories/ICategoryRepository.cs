﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(int id);
        IQueryable<Category> GetCategoryByProductsAsync();
    }

}
