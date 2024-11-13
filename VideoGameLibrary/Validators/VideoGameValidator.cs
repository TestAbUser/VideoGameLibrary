using VideoGameLibrary.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace VideoGameLibrary.Validators
{
    public class VideoGameValidator : AbstractValidator<VideoGame>
    {
        public VideoGameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required")
                                .Custom((name, context) =>
                                {
                                    Regex rg = new Regex("<.*?>"); // try to match HTML tags
                                    if (rg.Matches(name).Count > 0)
                                    {
                                        // Raises an error
                                        context.AddFailure(new ValidationFailure("Name", "The Name parameter has invalid content"));
                                    }
                                });

        }
    }
}
