using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Ürün adı boş olamaz.")
                .NotNull()
                    .WithMessage("Ürün adı boş olamaz.")
                .MinimumLength(2)
                    .WithMessage("Ürün adı en az 2, en fazla 100 karakter olabilir.")
                .MaximumLength(100)
                    .WithMessage("Ürün adı en az 2, en fazla 100 karakter olabilir.")
                .Matches(@"^[a-zA-Z0-9\s]+$")
                    .WithMessage("Ürün adı yalnızca harf, rakam ve boşluk içerebilir.");
            
            RuleFor(p => p.Stock)
                .NotEmpty()
                    .WithMessage("Stok adedi boş olamaz.")
                .NotNull()
                    .WithMessage("Stok adedi boş olamaz.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Stok adedi 0'dan küçük olamaz.")
                .Must(stock => stock == (int)stock)
                    .WithMessage("Stok adedi tam sayı olmalıdır.");

            RuleFor(p => p.Price)
                .NotEmpty()
                    .WithMessage("Fiyat boş olamaz.")
                .NotNull()
                    .WithMessage("Fiyat boş olamaz.")
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Fiyat 0'dan küçük olamaz.")
                .LessThanOrEqualTo(10000)
                    .WithMessage("Fiyat 10.000 TL'den fazla olamaz.");

        }
    }
}
