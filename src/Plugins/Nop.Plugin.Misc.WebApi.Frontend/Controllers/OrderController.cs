using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.WebApi.Frontend.Dto.Orders;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Orders;

namespace Nop.Plugin.Misc.WebApi.Frontend.Controllers;

public partial class OrderController : BaseNopWebApiController
{
    #region Fields

    protected readonly ICustomerService _customerService;
    protected readonly IOrderService _orderService;
    protected readonly IDateTimeHelper _dateTimeHelper;
    protected readonly IPriceFormatter _priceFormatter;

    #endregion

    #region Ctor

    public OrderController(
        ICustomerService customerService,
        IOrderService orderService,
        IDateTimeHelper dateTimeHelper,
        IPriceFormatter priceFormatter
        )
    {
        _customerService = customerService;
        _orderService = orderService;
        _dateTimeHelper = dateTimeHelper;
        _priceFormatter = priceFormatter;
    }

    #endregion

    #region Methods

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(CustomerOrderDetailsModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomerOrderDetailsModelDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> OrderDetails(string email)
    {
        var cutomer = await _customerService.GetCustomerByEmailAsync(email);
        if (cutomer == null)
            return new JsonResult(new { success = false, message = "Customer Not Found", data = string.Empty }) { StatusCode = StatusCodes.Status404NotFound };
        var orders = await _orderService.SearchOrdersAsync(customerId: cutomer.Id);
        var ordersDto = new List<CustomerOrderDetailsModelDto>();
        foreach (var order in orders)
        {
            var orderDto = new CustomerOrderDetailsModelDto(order.Id, await _priceFormatter.FormatPriceAsync(order.OrderTotal, true, false), await _dateTimeHelper.ConvertToUserTimeAsync(order.CreatedOnUtc, DateTimeKind.Utc));
            ordersDto.Add(orderDto);
        }
        return new JsonResult(new { success = true, message = "Success", data = ordersDto }) { StatusCode = StatusCodes.Status200OK };
    }

    #endregion
}