using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Plugins;

namespace Nop.Plugin.DiscountRules.CustomDiscounts;

public class CustomDiscountsDiscountRequirementRule : BasePlugin, IDiscountRequirementRule
{
    #region Fields

    protected readonly IActionContextAccessor _actionContextAccessor;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly ILocalizationService _localizationService;
    protected readonly IOrderService _orderService;
    protected readonly ISettingService _settingService;
    protected readonly IUrlHelperFactory _urlHelperFactory;
    protected readonly IWebHelper _webHelper;

    #endregion

    #region Ctor

    public CustomDiscountsDiscountRequirementRule(IActionContextAccessor actionContextAccessor,
        IDiscountService discountService,
        ICustomerService customerService,
        ILocalizationService localizationService,
        IOrderService orderService,
        ISettingService settingService,
        IUrlHelperFactory urlHelperFactory,
        IWebHelper webHelper)
    {
        _actionContextAccessor = actionContextAccessor;
        _customerService = customerService;
        _discountService = discountService;
        _localizationService = localizationService;
        _orderService = orderService;
        _settingService = settingService;
        _urlHelperFactory = urlHelperFactory;
        _webHelper = webHelper;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Check discount requirement
    /// </summary>
    /// <param name="request">Object that contains all information required to check the requirement (Current customer, discount, etc)</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the result
    /// </returns>
    public async Task<DiscountRequirementValidationResult> CheckRequirementAsync(DiscountRequirementValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        //invalid by default
        var result = new DiscountRequirementValidationResult();

        if (request.Customer == null)
            return result;

        //try to get saved restricted no of orders placed
        var noOfOrderPlaced = await _settingService.GetSettingByKeyAsync<int>(string.Format(DiscountRequirementDefaults.SettingsKey, request.DiscountRequirementId));
        if (noOfOrderPlaced == 0)
            return result;

        var customerOrdersCount = (await _orderService.SearchOrdersAsync(customerId: request.Customer.Id)).ToArray().Length;

        //result is valid if the customer placed orders
        result.IsValid = noOfOrderPlaced <= customerOrdersCount;

        return result;
    }

    /// <summary>
    /// Get URL for rule configuration
    /// </summary>
    /// <param name="discountId">Discount identifier</param>
    /// <param name="discountRequirementId">Discount requirement identifier (if editing)</param>
    /// <returns>URL</returns>
    public string GetConfigurationUrl(int discountId, int? discountRequirementId)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

        return urlHelper.Action("Configure", "DiscountRulesCustomDiscounts",
            new { discountId = discountId, discountRequirementId = discountRequirementId }, _webHelper.GetCurrentRequestProtocol());
    }

    /// <summary>
    /// Install the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task InstallAsync()
    {
        //locales
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.DiscountRules.CustomDiscounts.Fields.NoOfOrderPlaced"] = "Required No. Of Order Palced",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.NoOfOrderPlaced.Hint"] = "Discount will be applied if customer has already placed no of orders.",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.NoOfOrderPlacedId.Required"] = "No. Of Order Palced is required",
            ["Plugins.DiscountRules.CustomDiscounts.Fields.DiscountId.Required"] = "Discount is required"
        });

        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstall the plugin
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public override async Task UninstallAsync()
    {
        //discount requirements
        var discountRequirements = (await _discountService.GetAllDiscountRequirementsAsync())
            .Where(discountRequirement => discountRequirement.DiscountRequirementRuleSystemName == DiscountRequirementDefaults.SystemName);
        foreach (var discountRequirement in discountRequirements)
        {
            await _discountService.DeleteDiscountRequirementAsync(discountRequirement, false);
        }

        //locales
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.DiscountRules.CustomDiscounts");

        await base.UninstallAsync();
    }

    #endregion
}