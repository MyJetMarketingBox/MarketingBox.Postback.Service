using FluentValidation;

namespace MarketingBox.Postback.Service.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected BaseValidator()
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }
    }
}