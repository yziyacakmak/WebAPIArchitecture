using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;

namespace App.Services.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLower()));

            CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name,
    opt => opt.MapFrom(src => src.Name.ToLower()));
        }
    }
}
