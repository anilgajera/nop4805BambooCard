
namespace Nop.Plugin.DiscountRules.CustomDiscounts;

/// <summary>
/// Represents defaults for the discount requirement rule
/// </summary>
public static class DiscountRequirementDefaults
{
    /// <summary>
    /// The system name of the discount requirement rule
    /// </summary>
    public static string SystemName => "DiscountRequirement.CustomDiscounts";

    /// <summary>
    /// The key of the settings to save restricted orders
    /// </summary>
    public static string SettingsKey => "DiscountRequirement.CustomDiscounts-{0}";

    /// <summary>
    /// The HTML field prefix for discount requirements
    /// </summary>
    public static string HtmlFieldPrefix => "DiscountRulesCustomDiscounts{0}";
}