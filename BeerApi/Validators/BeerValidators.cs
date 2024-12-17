using FluentValidation;
using BeerApi.DTOs;

namespace BeerApi.Validators;

public class CreateBeerDtoValidator : AbstractValidator<CreateBeerDto>
{
    public CreateBeerDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters")
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters long");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type is required")
            .MaximumLength(50)
            .WithMessage("Type cannot exceed 50 characters")
            .MinimumLength(2)
            .WithMessage("Type must be at least 2 characters long");
    }
}

public class AddRatingDtoValidator : AbstractValidator<AddRatingDto>
{
    public AddRatingDtoValidator()
    {
        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");
    }
} 