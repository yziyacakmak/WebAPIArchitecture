﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryDtoWithProducts>> GetCategoryWithProductsAsync(int id);
        Task<ServiceResult<List<CategoryDtoWithProducts>>> GetCategoryWithProductsAsync();
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
