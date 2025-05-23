﻿using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;

namespace App.Services.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();

        CreateMap<Category, CategoryDtoWithProducts>().ReverseMap();

        CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.Name.ToLower()));

        CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
    }

}

