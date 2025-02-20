using System.Net;
using App.Repositories.Products;
using App.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async  Task<IActionResult> GetAll()=> CreateActionResult(await productService.GetAllListAsync());
    
    [HttpGet]
    public async  Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)=>CreateActionResult(await productService.CreateAsync(request));
    
    [HttpPut]
    public async Task<IActionResult> Update(int id,UpdateProductRequest request)=>CreateActionResult(await productService.UpdateAsync(id,request));
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
}