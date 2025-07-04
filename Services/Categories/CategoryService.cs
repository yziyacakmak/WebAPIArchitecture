﻿using System.Net;
using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories;
public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await categoryRepository.GetAll().ToListAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult<CategoryDto?>.Fail("Category not found", HttpStatusCode.NotFound);
        }
        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto)!;
    }

    public async Task<ServiceResult<CategoryDtoWithProducts>> GetCategoryWithProductsAsync(int id)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(id);
        if (category is null)
        {
            return ServiceResult<CategoryDtoWithProducts>.Fail("Category not found", HttpStatusCode.NotFound);
        }
        var categoryAsDto = mapper.Map<CategoryDtoWithProducts>(category);
        return ServiceResult<CategoryDtoWithProducts>.Success(categoryAsDto)!;
    }

    public async Task<ServiceResult<List<CategoryDtoWithProducts>>> GetCategoryWithProductsAsync()
    {
        var categories = await categoryRepository.GetCategoryWithProducts().ToListAsync();

        var categoriesAsDto = mapper.Map<List<CategoryDtoWithProducts>>(categories);

        return ServiceResult<List<CategoryDtoWithProducts>>.Success(categoriesAsDto)!;
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.GetAll().AnyAsync(p => p.Name == request.Name);

        if (anyCategory)
        {
            return ServiceResult<int>.Fail("Category name must be unique");
        }

        var newCategory = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.SuccessAsCreated(newCategory.Id,$"api/categories/{newCategory.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.GetAll().AnyAsync(p => p.Name == request.Name && p.Id != id);

        if (anyCategory)
        {
            return ServiceResult.Fail("Category name must be unique");
        }

        var category = mapper.Map<Category>(request);
        category.Id = id;

        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);


    }
}



