using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.DiscountRules.CustomDiscounts.Models;

public class RequirementModel
{
    public RequirementModel()
    {
    }

    [NopResourceDisplayName("Plugins.DiscountRules.CustomDiscounts.Fields.NoOfOrderPlaced")]
    public int NoOfOrderPlaced { get; set; }

    public int DiscountId { get; set; }

    public int RequirementId { get; set; }

}