using FluentValidation;
using FluentValidation.Results;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Models.Validator
{
    public class AddWalkRequestValidator:AbstractValidator<Models.DTO.AddWalkRequest>
    {
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public AddWalkRequestValidator(IRegionRepository regionRepository,
            IWalkDifficultyRepository walkDifficultyRepository)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        protected override bool PreValidate(ValidationContext<AddWalkRequest> context, ValidationResult result)
        {
            //return base.PreValidate(context, result);
            var region = regionRepository.Get(context.InstanceToValidate.RegionId).Result;
            if (region == null)
            {
                result.Errors.Add(new ValidationFailure("Region", "Region must not be existing one."));
            }

            var wd = walkDifficultyRepository.Get(context.InstanceToValidate.WalkDifficultyId).Result;
            if (wd == null)
            {
                result.Errors.Add(new ValidationFailure("Walk Difficulty", "Walk Difficulty must not be existing one."));
            }
            return false;
        }
    }
}
