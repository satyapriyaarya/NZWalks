using FluentValidation;

namespace NZWalks.API.Models.Validator
{
    public class AddRegionRequestValidator:AbstractValidator<Models.DTO.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
