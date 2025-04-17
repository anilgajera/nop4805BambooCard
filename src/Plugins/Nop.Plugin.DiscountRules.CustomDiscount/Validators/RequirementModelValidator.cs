using FluentValidation;
using Nop.Plugin.DiscountRules.CustomDiscounts.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Validators;

/// <summary>
/// Represents an <see cref="RequirementModel"/> validator.
/// </summary>
public class RequirementModelValidator : BaseNopValidator<RequirementModel>
{
    public RequirementModelValidator(ILocalizationService localizationService)
    {
        RuleFor(model => model.DiscountId)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.DiscountRules.CustomDiscounts.Fields.DiscountId.Required"));
        RuleFor(model => model.NoOfOrderPlaced)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.DiscountRules.CustomDiscounts.Fields.NoOfOrderPlacedId.Required"));
    }
}