using FluentValidation;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Validators
{
    public class CreateOrUpdateReferenceRequestValidator : BaseValidator<CreateOrUpdateReferenceRequest>
    {
        public CreateOrUpdateReferenceRequestValidator()
        {
            RuleFor(x => x.AffiliateId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.HttpQueryType)
                .NotNull();
        }
    }
}