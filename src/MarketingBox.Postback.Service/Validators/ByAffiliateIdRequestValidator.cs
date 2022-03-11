using FluentValidation;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Validators
{
    public class ByAffiliateIdRequestValidator : BaseValidator<ByAffiliateIdRequest>
    {
        public ByAffiliateIdRequestValidator()
        {
            RuleFor(x => x.AffiliateId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);
        }
    }
}