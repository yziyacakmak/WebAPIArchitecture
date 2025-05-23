using App.Services.Products;

namespace App.Services.Categories.Dto;

public record CategoryDtoWithProducts(int Id, string Name, List<ProductDto> Products);

