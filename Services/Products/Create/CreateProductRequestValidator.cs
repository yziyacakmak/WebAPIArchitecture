using App.Repositories.Products;
using FluentValidation;

namespace App.Services.Products.Create
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name must not be empty.")
                .Length(3, 10).WithMessage("Product name must between 3 and 10 characters.");
            //.Must(MustUniqueProductName).WithMessage("Product name must be unique.");


            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");


            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category Id must be greater than 0.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(1).WithMessage("Product stock must be greater than or equal to 1.")
                .LessThanOrEqualTo(100).WithMessage("Product stock must be less than or equal to 100.");
        }

        //sync validation

        //private bool MustUniqueProductName(string name)
        //{
        //    return !_productRepository.Where(x=>x.Name== name).Any();
        //}
    }
}
