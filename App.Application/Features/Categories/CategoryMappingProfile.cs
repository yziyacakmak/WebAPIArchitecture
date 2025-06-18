﻿using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

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

