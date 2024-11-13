using VideoGameLibrary.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace VideoGameLibrary.Validators
{
    public class VideoGamePatchValidator : AbstractValidator<VideoGamePatch>
    {
        public VideoGamePatchValidator()
        {
            RuleFor(x => x.Genres).NotEmpty().WithMessage("{ParameterName} cannot be empty")
                                .Custom((genres, context) =>
                                {
                                    Regex rg = new Regex("<.*?>"); // try to match HTML tags
                                    if (rg.Matches(genres).Count > 0)
                                    {
                                        // Raises an error
                                        context.AddFailure(new ValidationFailure("Genres", "The genres have invalid content"));
                                    }
                                });

        }
    }
}
