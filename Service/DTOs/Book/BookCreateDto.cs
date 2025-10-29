using FluentValidation;

namespace Service.DTOs.Book
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int SeriesNumber { get; set; }
    }

    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Name is required.");
        }
    }
}
