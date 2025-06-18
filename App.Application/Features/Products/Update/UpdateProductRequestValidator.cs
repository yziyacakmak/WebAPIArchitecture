using FluentValidation;

namespace App.Application.Features.Products.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{

    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
.NotEmpty().WithMessage("Product name must not be empty.")
.Length(3, 10).WithMessage("Product name must between 3 and 10 characters.");



        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");


        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category Id must be greater than 0.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(1).WithMessage("Product stock must be greater than or equal to 1.")
            .LessThanOrEqualTo(100).WithMessage("Product stock must be less than or equal to 100.");
    }

}

