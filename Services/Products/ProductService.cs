﻿using System.Net;
using System.Runtime.InteropServices;
using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork,IMapper mapper):IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            

            var products = await productRepository.GetTopPriceProductsAsync(count);
            var productsAsDto = products.Select(p => new ProductDto(p.Id,p.Name,p.Price,p.Stock))
                .ToList();

            return ServiceResult<List<ProductDto>>.Success((productsAsDto));
        }
        
        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products =  await productRepository.GetAll().ToListAsync();
            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success((productsAsDto));
        }
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber,int pageSize)
        {
            var products = await productRepository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productsAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success((productsAsDto));
        }
        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found",HttpStatusCode.NotFound);
            }
            var productsAsDto = mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(productsAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var anyProduct = await productRepository.GetAll()
                .AnyAsync(p => p.Name == request.Name);

            if(anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Product name must be unique");
            }

            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),
                $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {

            // fast fail 
            // guard clauses
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStock(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }




    }
}
