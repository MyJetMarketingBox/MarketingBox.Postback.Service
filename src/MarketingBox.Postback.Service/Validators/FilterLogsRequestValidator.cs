using FluentValidation;
using MarketingBox.Postback.Service.Domain.Models.Requests;

namespace MarketingBox.Postback.Service.Validators
{
    public class FilterLogsRequestValidator : BaseValidator<FilterLogsRequest>
    {
        public FilterLogsRequestValidator()
        {
            RuleFor(x => x.AffiliateId)
                .GreaterThan(0)
                .When(x => x.AffiliateId.HasValue);
            
            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue);
        }
    }
}