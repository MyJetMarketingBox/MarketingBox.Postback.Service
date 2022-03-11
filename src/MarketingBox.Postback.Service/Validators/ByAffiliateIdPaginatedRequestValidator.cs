using FluentValidation;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Validators
{
    public class ByAffiliateIdPaginatedRequestValidator : BaseValidator<ByAffiliateIdPaginatedRequest>
    {
        public ByAffiliateIdPaginatedRequestValidator()
        {
            RuleFor(x => x.AffiliateId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0);
        }
    }
}